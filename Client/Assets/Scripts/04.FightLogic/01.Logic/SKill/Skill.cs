using System;
using PEMath;
using UnityEngine;

public class Skill
{
    public int skillId;
    public SkillCfg skillCfg;
    /// <summary>
    /// 技能偏移参数：从UI轮盘转换成的地图偏移量
    /// </summary>
    public PEVector3 skillArgs;
    public MainLogicUnit lockTarget;
    public SkillState skillState = SkillState.None;
    
    /// 施法时间
    public PEInt spellTime;
    /// 技能总时间
    public PEInt skillTime;
    
    public MainLogicUnit owner;

    /// <summary>
    /// 回到free动画，被中断技能或者取消后摇会到这个动作
    /// </summary>
    public Action FreeAniCallback;
    /// <summary>
    /// 释放成功得callback
    /// </summary>
    public Action<Skill> SpellSuccessBp;

    public Skill(int skillId,MainLogicUnit owner)
    {
        this.skillId = skillId;
        skillCfg = ResSvc.Instance().GetSkillConfigById(this.skillId);
        spellTime = skillCfg.spellTime;
        skillTime = skillCfg.skillTime;
        
        if (skillCfg.isNormalAttack)
        {
            owner.InitAttackSpeedRate(1000/skillTime);
        }
        
        this.owner = owner;
    }

    void HitTarget(MainLogicUnit target, object[] args = null)
    {
        //音效表现
        if (skillCfg.audio_hit != null)
        {
            target.PlayAudio(skillCfg.audio_hit);
        } 
        
        //可能全为buff伤害，这里为0
        if (skillCfg.damage != 0)
        {
            PEInt damage = skillCfg.damage;
            target.GetDamageBySkill(damage,this);
        }
        
        // 附加buff(To Target or Bullet)
        if (skillCfg.buffIdArr == null)
        {
            return;
        }
        
        for(int i = 0; i < skillCfg.buffIdArr.Length; i++) {
            int buffID = skillCfg.buffIdArr[i];
            if(buffID == 0) {
                this.Warn($"SkillID:{skillCfg.skillId} exist buffID == 0,check your buffID Configs");
                continue;
            }
            BuffCfg buffCfg = ResSvc.Instance().GetBuffConfigById(buffID);
            if(buffCfg.attacher == AttachTypeEnum.Target || buffCfg.attacher == AttachTypeEnum.Bullet) {
                target.CreateSkillBuff(owner, this, buffID, args);
            }
        }
    }
    

    /// <summary>
    /// 技能生效
    /// </summary>
    void CalcSkillAttack(MainLogicUnit lockTarget)
    {
        if (skillCfg.bulletCfg != null)
        {
            
        }
        else
        {
            HitTarget(lockTarget);
        }
    }

    /// <summary>
    /// 施法前摇
    /// </summary>
    void SkillSpellStart(PEVector3 spellDir)
    {
        // 0.切换技能状态
        skillState = SkillState.SpellStart;
        // 1.播放音效
        if (skillCfg.audio_start != null)
        {
            owner.PlayAudio(skillCfg.audio_start);
        }
        // 2.修改朝向
        if (spellDir != PEVector3.zero)
        {
            owner.mainViewUnit.UpdateSKillRotation(spellDir);
        }
        // 3.播放动画
        if (skillCfg.aniName != null)
        {
            owner.InputFakeMoveKey(PEVector3.zero);// 释放技能所以先取消移动
            owner.PlayAni(skillCfg.aniName);
            owner.ClearFreeAniCallBack();
            // 技能被中断或后摇被移动取消需要调用动画重置
            FreeAniCallback = () => {
                owner.PlayAni("free");
            };
        }
    }

    /// <summary>
    /// 技能释放完成
    /// </summary>
    void SkillSpellAfter()
    {
        skillState = SkillState.SpellAfter;
        if (skillCfg.audio_work != null)
        {
            owner.PlayAudio(skillCfg.audio_work);
        }
        
        // TODO 施法成功，消耗相应资源
        if (owner.IsPlayerSelf() && !skillCfg.isNormalAttack)
        {
            // 进入技能cd
            BattleSys.Instance.EnterCDState(skillId,skillCfg.cdTime);
        }
        
        // 技能释放成功回调，以供提供事件给buff使用(比如累计3次普攻有特效)
        SpellSuccessBp?.Invoke(this);

        // 恢复原先玩家输入的方向信息
        if (skillCfg.aniName != null)
        {
            owner.RecoverUIInput();
        }
        
        // 启动定时器，在后摇完成后的时间点，将技能状态重置为null（因此闪现无法重置普攻）
        if (skillTime > spellTime)
        {
            owner.CreateLogicTimer(SkillEnd,skillTime - spellTime);
        }
        else
        {
            // 技能总时长小于了施法时间，再去用定时器没意义
            SkillEnd();
        }
    }
    
    /// <summary>
    /// 施法后摇动作完成，角色切换到Idle状态
    /// </summary>
    void SkillEnd()
    {
        // 如果不是SpellAfter说明技能没有释放成功，具体看 SkillWork() 代码
        if(skillState == SkillState.None || skillState == SkillState.SpellStart) {
            if(owner.IsPlayerSelf()) {
                // TODO 下面的if条件是什么原因?
                if(skillCfg.targetCfg != null
                   && skillCfg.targetCfg.targetTeam == TargetTeamEnum.Enemy
                   && skillCfg.targetCfg.searchDis > 0) {
                    Buff mf = owner.GetBuffById(ClientConfig.CommonMoveAttackBuffId);
                    if(mf != null) {
                        // 如果已存在移动buff，就结束过去的，添加新的。从而保持只有一个起效。
                        mf.unitState = SubUnitState.End;
                    }
        
                    // 技能未施放成功，添加通用移动攻击buff
                    owner.CreateSkillBuff(owner, this, ClientConfig.CommonMoveAttackBuffId);
                }
            }
        }
        
        if (FreeAniCallback != null)
        {
            FreeAniCallback.Invoke();
            FreeAniCallback = null;
        }

        skillState = SkillState.None;
        lockTarget = null;
    }

    public void ReleaseSkill(PEVector3 skillArgs)
    {
        this.skillArgs = skillArgs;
        // 目标技能，必须存在施法目标，且目标队伍类型不能为动态类型
        if (skillCfg.targetCfg != null && skillCfg.targetCfg.targetTeam != TargetTeamEnum.Dynamic)
        {
            lockTarget = CalcRule.FindSingleTargetByRule(owner, skillCfg.targetCfg, skillArgs);
            if (lockTarget != null)
            {
                PEVector3 spellDir = lockTarget.LogicPos - owner.LogicPos;
                SkillSpellStart(spellDir);
                
                void SkillWork()
                {
                    CalcSkillAttack(lockTarget);
                    // 附着buff
                    AttachSkillBuffToCaster();
                    SkillSpellAfter();
                } 
                
                
                if (spellTime == 0)
                {
                    // 立即生效
                    SkillWork();
                }
                else
                {
                    // 定时处理
                    void DelaySkillWork()
                    {
                        lockTarget = CalcRule.FindSingleTargetByRule(owner, skillCfg.targetCfg, skillArgs);
                        if (lockTarget != null)
                        {
                            // 如果目标还在范围内
                            SkillWork();
                        }
                        else
                        {
                            SkillEnd();
                        }
                    }
                    
                    owner.CreateLogicTimer(DelaySkillWork,spellTime);
                }
                
                FreeAniCallback = () =>
                {
                    owner.PlayAni("free");
                };
            }
            else
            {
                // 没有符合条件的技能目标，本次技能释放结束
                SkillEnd();
            }
        }
        // 非目标技能
        else
        {
            SkillSpellStart(skillArgs);
            
            if(spellTime == 0) {
                if(skillCfg.bulletCfg != null) {
                    DirectionBullet();
                }
                AttachSkillBuffToCaster();
                SkillSpellAfter();
            }
            else {
                owner.CreateLogicTimer(() => {
                    if(skillCfg.bulletCfg != null) {
                        DirectionBullet();
                    }
                    AttachSkillBuffToCaster();
                    SkillSpellAfter();
                }, spellTime);
            }
            
            void DirectionBullet() {
                //非目标弹道技能
                //TODO
            }
        }
    }

    void AttachSkillBuffToCaster()
    {
        if (skillCfg.buffIdArr == null)
        {
            return;
        }

        for (int i = 0; i < skillCfg.buffIdArr.Length; i++)
        {
            int buffId = skillCfg.buffIdArr[i];
            if (buffId == 0)
            {
                this.Warn("SkillId: "+ skillCfg.skillId +" buffId = 0,Check it.");
                continue;
            }
            
            // buff(To Caster or Indie)
            BuffCfg buffCfg = ResSvc.Instance().GetBuffConfigById(buffId);
            if(buffCfg.attacher == AttachTypeEnum.Caster || buffCfg.attacher == AttachTypeEnum.Indie) {
                owner.CreateSkillBuff(owner, this, buffId);
            }
        }
    }

    /// 技能替换
    public void ReplaceSkillCfg(int replaceId) {
        if(skillId == replaceId) {
            this.Log("Set replaceId == skillId:" + replaceId);
        }

        skillCfg = ResSvc.Instance().GetSkillConfigById(replaceId);
        spellTime = skillCfg.spellTime;
        skillTime = skillCfg.skillTime;
        if(skillCfg.isNormalAttack) {
            owner.InitAttackSpeedRate(1000 / skillTime);
        }
    }
}

public enum SkillState
{
    None,
    SpellStart,// 前摇
    SpellAfter,// 后摇
}