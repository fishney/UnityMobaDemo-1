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
    }
}

public enum SKillState
{
    None,
    SpellStart,// 前摇
    SpellAfter,// 后摇
}