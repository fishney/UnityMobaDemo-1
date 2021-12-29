using System;
using PEMath;
using UnityEngine;

public class Skill
{
    public int skillId;
    public SkillCfg skillCfg;
    public PEVector3 skillArgs;
    public MainLogicUnit lockTarget;
    public SKillState skillState = SKillState.None;
    
    /// 施法时间
    public PEInt spellTime;
    /// 技能总时间
    public PEInt skillTime;
    
    public MainLogicUnit owner;

    /// <summary>
    /// 回到free动画，被中断技能或者取消后摇会到这个动作
    /// </summary>
    public Action FreeAniCallback;

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
        if (skillCfg.audio_hit != null)
        {
            target.PlayAudio(skillCfg.audio_hit);
        }

        if (skillCfg.damage != 0)
        {
            PEInt damage = skillCfg.damage;
            target.GetDamageBySkill(damage,this);
        }
        // 附加buff
        if (skillCfg.buffIdArr == null)
        {
            return;
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
        skillState = SKillState.SpellStart;
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
            // 技能被中断或后摇被移动取消需要调用动画重置
        }
    }

    /// <summary>
    /// 技能释放完成
    /// </summary>
    void SkillSpellAfter()
    {
        skillState = SKillState.SpellAfter;
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
        if (FreeAniCallback != null)
        {
            FreeAniCallback.Invoke();
            FreeAniCallback = null;
        }

        skillState = SKillState.None;
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
                
                // 立即生效
                void SkillWork()
                {
                    CalcSkillAttack(lockTarget);
                    // 附着buff
                    AttachSkillBuffToCaster();
                    SkillSpellAfter();
                }

                if (spellTime == 0)
                {
                    
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
            
            // TODO buff
        }
    }
}

public enum SKillState
{
    None,
    SpellStart,// 前摇
    SpellAfter,// 后摇
}