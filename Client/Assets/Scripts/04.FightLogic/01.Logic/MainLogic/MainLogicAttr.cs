/****************************************************
    文件：MainLogicAttr.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using PEMath;

/// <summary>
/// MainLogicAttributes
/// </summary>
public partial class MainLogicUnit
{
    /// <summary>
    /// 状态改变回调
    /// para:State, isShow
    /// </summary>
    public Action<StateEnum, bool> OnStateChange;
    /// <summary>
    /// 血量变化 飘字回调
    /// </summary>
    public Action<int, JumpUpdateInfo> OnHPChange;
    /// <summary>
    /// 受到伤害回调
    /// </summary>
    public Action OnHurt;
    /// <summary>
    /// 死亡时回调
    /// </summary>
    public Action<MainLogicUnit> OnDeath;
    /// <summary>
    /// 被减速时
    /// </summary>
    public Action<JumpUpdateInfo> OnSlowDown;

    private PEInt _hp;
    public PEInt Hp
    {
        private set 
        {
            _hp = value;
        }
        get
        {
            return _hp;
        }
    }
    
    private PEInt _def;
    public PEInt Def
    {
        private set 
        {
            _def = value;
        }
        get
        {
            return _def;
        }
    }

    /// 一秒攻击多少次的速率，无buff角色基础值
    public PEInt AttackSpeedRateBase;
    
    private PEInt _attackSpeedRateCurrent;
    /// 一秒攻击多少次的速率，计算buff
    public PEInt AttackSpeedRateCurrent
    {
        private set 
        {
            _attackSpeedRateCurrent = value;

            Skill skill = GetNormalSkill();
            // 新攻击动画时间 = 原攻击动画时间 * 新频率(1秒n次)/原频率(1秒n次)
            skill.skillTime = skill.skillCfg.skillTime * AttackSpeedRateBase / _attackSpeedRateCurrent;
            skill.spellTime = skill.skillCfg.spellTime * AttackSpeedRateBase / _attackSpeedRateCurrent;
            
        }
        get
        {
            return _attackSpeedRateCurrent;
        }
    }

    #region 状态改变

    // -------------沉默：沉默时无法施放技能-------------
    int silenceCount;
    public int SilenceCount {
        get {
            return silenceCount;
        }
        set {
            silenceCount = value;
            if(IsSilenced()) {
                // 1.进入沉默状态并触发OnStateChange
                OnStateChange?.Invoke(StateEnum.Silenced, true);
            }
            else {
                OnStateChange?.Invoke(StateEnum.Silenced, false);
            }
        }
    }
    
    bool IsSilenced() {
        return silenceCount != 0;
    }
    
    // -------------晕眩：无法移动，无法施放技能（包括普攻），可以被水银净化解控(水银未实现)-------------
    int stunnedCount;
    public int StunnedCount {
        get {
            return stunnedCount;
        }
        set {
            stunnedCount = value;
            if(IsStunned()) {
                // 1.立即停止移动
                InputFakeMoveKey(PEVector3.zero);
                // 2.进入眩晕状态并触发OnStateChange
                OnStateChange?.Invoke(StateEnum.Stunned, true);
            }
            else {
                OnStateChange?.Invoke(StateEnum.Stunned, false);
            }
        }
    }
    bool IsStunned() {
        return stunnedCount != 0;
    }

    // -------------击飞：无法移动，无法施放技能（包括普攻）,无法被水银净化解控(水银未实现)-------------
    int knockupCount;
    public int KnockupCount {
        get {
            return knockupCount;
        }
        set {
            knockupCount = value;
            if(IsKnockup()) {
                // 1.立即停止移动
                InputFakeMoveKey(PEVector3.zero);
                // 2.进入击飞状态并触发OnStateChange
                OnStateChange?.Invoke(StateEnum.Knockup, true);
                // 3.表现上得高度变更
                LogicPos += new PEVector3(0, (PEInt)0.5F, 0);
            }
            else {
                OnStateChange?.Invoke(StateEnum.Knockup, false);
                // 表现上得高度变更回来
                LogicPos += new PEVector3(0, (PEInt)(-0.5F), 0);
            }
        }
    }
    bool IsKnockup() {
        return knockupCount != 0;
    }
    #endregion
    


    void InitProperties()
    {
        Hp = ud.unitCfg.hp;
        Def = ud.unitCfg.def;
    }

    public void InitAttackSpeedRate(PEInt rate)
    {
        AttackSpeedRateBase = rate;
        _attackSpeedRateCurrent = rate;
    }

    public bool IsTeam(TeamEnum teamEnum)
    {
        return ud.teamEnum == teamEnum;
    }

    #region API Func

    public void GetDamageBySkill(PEInt damage,Skill skill)
    {
        OnHurt?.Invoke();// 比如挂载亚瑟的受击标记buff，受伤有额外伤害。
        PEInt hurt = damage - Def;
        if (hurt > 0)
        {
            Hp -= hurt;
            if (Hp <= 0)
            {
                Hp = 0;
                unitState = UnitStateEnum.Dead;
                InputFakeMoveKey(PEVector3.zero);
                OnDeath?.Invoke(skill.owner);
                PlayAni("death");
                this.Log($"{unitName} hp=0, Died.");
            }

            JumpUpdateInfo ji = null;
            if (IsPlayerSelf() || skill.owner.IsPlayerSelf())
            {
                ji = new JumpUpdateInfo()
                {
                    jumpVal = hurt.RawInt,
                    jumpType = JumpTypeEnum.SkillDamage,
                    jumpAni = JumpAniEnum.LeftCurve,
                };
            }
            OnHPChange?.Invoke(Hp.RawInt, ji);
        }
    }

    public void GetCureByBuff(PEInt cure,Buff buff)
    {
        if (Hp >= ud.unitCfg.hp)
        {
            // 血量溢出
            return;
        }

        var beforeCure = Hp;
        Hp = (Hp + cure) > ud.unitCfg.hp 
            ? ud.unitCfg.hp 
            : (Hp + cure);
        var realCure = Hp - beforeCure;

        JumpUpdateInfo jui = null;
        // 作用目标是英雄自己
        // buff来源是英雄自己
        // buff附着目标是英雄角色自己
        if (IsPlayerSelf() || buff.source.IsPlayerSelf() || buff.owner.IsPlayerSelf())
        {
            jui = new JumpUpdateInfo()
            {
                jumpVal = realCure.RawInt,
                jumpType = JumpTypeEnum.Cure,
                jumpAni = JumpAniEnum.CenterUp,
            };
        }
        
        OnHPChange?.Invoke(Hp.RawInt,jui);
    }
    
    /// <summary>
    /// GetDamageByBuff
    /// </summary>
    /// <param name="calcCB">是否需要再次触发OnHurt</param>
    public void GetDamageByBuff(PEInt damage, Buff buff, bool calcCB = true) {
        if(calcCB) {
            OnHurt?.Invoke();
        }
        if(buff.cfg.hitTickAudio != null) {
            PlayAudio(buff.cfg.hitTickAudio);
        }

        PEInt hurt = damage - Def;
        if(hurt > 0) {
            Hp -= hurt;
            if(Hp <= 0) {
                Hp = 0;
                unitState = UnitStateEnum.Dead;//状态切换
                InputFakeMoveKey(PEVector3.zero);
                OnDeath?.Invoke(buff.source);
                PlayAni("death");
            }

            JumpUpdateInfo jui = null;
            if(IsPlayerSelf() || buff.source.IsPlayerSelf() || buff.owner.IsPlayerSelf()) {
                jui = new JumpUpdateInfo {
                    jumpVal = hurt.RawInt,
                    jumpType = JumpTypeEnum.BuffDamage,
                    jumpAni = JumpAniEnum.RightCurve
                };
            }
            OnHPChange?.Invoke(Hp.RawInt, jui);
        }
    }

    /// <summary>
    /// 人物加速减速
    /// </summary>
    /// <param name="value">移速偏移量offset</param>
    /// <param name="jumpInfo">减速时是否跳字</param>
    public void ModifyMoveSpeed(PEInt value, Buff buff, bool jumpInfo) { 
        LogicMoveSpeed += value;
        
        if(value < 0 && jumpInfo) {
            // 减速跳字,加速不跳字
            JumpUpdateInfo jui = null;
            if(IsPlayerSelf()) {
                jui = new JumpUpdateInfo {
                    jumpType = JumpTypeEnum.SlowSpeed,
                    jumpAni = JumpAniEnum.CenterUp
                };
            }
            OnSlowDown?.Invoke(jui);
        }
    }

    #endregion
}