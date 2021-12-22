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

    #endregion
}