/****************************************************
    文件：MainLogicSkill.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HOKProtocol;
using PEMath;

/// <summary>
/// MainLogicSkill
/// </summary>
public partial class MainLogicUnit
{
    protected Skill[] skillArr;
    private List<LogicTimer> timerList;
    private List<Buff> buffList;
    
    void InitSkill()
    {
        int len = ud.unitCfg.skillArr.Length;
        skillArr = new Skill[len];
        timerList = new List<LogicTimer>();
        buffList = new List<Buff>();
        
        for (int i = 0; i < len; i++)
        {
            skillArr[i] = new Skill(ud.unitCfg.skillArr[i],this);
        }
        
        // 被动buff
        int[] pasvBuffArr = ud.unitCfg.pasvBuff;
        if (pasvBuffArr?.Length > 0)
        {
            foreach (var pBuff in pasvBuffArr)
            {
                CreateSkillBuff(this, null, pBuff);
            }
        }

        OnDirChange += ClearFreeAniCallBack;
    }
    
    void TickSkill()
    {
        // buffList
        for (int i = buffList.Count - 1; i >= 0; --i)
        {
            Buff buff = buffList[i];
            if (buff.unitState == SubUnitState.None)
            {
                buff.LogicUnInit();
                buffList.RemoveAt(i);
            }
            else
            {
                buffList[i].LogicTick();
            }
        }
        
        // timerList
        for (int i = timerList.Count - 1; i >= 0; --i)
        {
            LogicTimer timer = timerList[i];
            if (timer.IsActive)
            {
                timer.TickTimer();
            }
            else
            {
                timerList.RemoveAt(i);
            }
        }
    }
    
    void UnInitSkill()
    {
        
    }

    void InputSkillKey(SkillKey key)
    {
        for (int i = 0; i < skillArr.Length; i++)
        {
            if (skillArr[i].skillId == key.skillId)
            {
                PEInt x = PEInt.zero;
                PEInt z = PEInt.zero;
                x.ScaledValue = key.x_val;
                z.ScaledValue = key.z_val;
                PEVector3 skillArgs = new PEVector3(x, 0, z);
                skillArr[i].ReleaseSkill(skillArgs);
                return;
            }
        }
        this.Error("skillId "+key.skillId+" is not exist.");
    }

    public Buff CreateSkillBuff(MainLogicUnit source, Skill skill, int buffId, object[] args = null)
    {
        Buff buff = ResSvc.Instance().CreateBuff(source, this, skill, buffId, args);
        buff.LogicInit();
        buffList.Add(buff);
        
        return buff;
    }

    #region API Func

    public Skill GetNormalSkill()
    {
        return skillArr[0];
    }
    
    public Skill GetSkillByID(int skillID)
    {
        return skillArr.FirstOrDefault(o=>o.skillId == skillID);
    }

    /// <summary>
    /// 是否在施展某技能前摇阶段
    /// </summary>
    public bool IsSkillSpelling() {
        for(int i = 0; i < skillArr.Length; i++) {
            if(skillArr[i].skillState == SkillState.SpellStart) {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// 是否某skillID的技能已准备完毕
    /// </summary>
    private bool IsSkillReady(int skillID) {
        for(int i = 0; i < skillArr.Length; i++) {
            if(skillArr[i].skillId == skillID) {
                return skillArr[i].skillState == SkillState.None;
            }
        }
        this.Warn("skill id config error.");
        return false;
    }
    
    /// <summary>
    /// 是否可以施放技能
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public bool CanReleaseSkill(int skillID) {
        return IsSilenced() == false
               && IsStunned() == false
               && IsKnockup() == false
               && IsSkillSpelling() == false
               && IsSkillReady(skillID);
    }
    
    /// <summary>
    /// 是否禁止施放所有技能
    /// </summary>
    /// <returns></returns>
    public bool IsForbidAllSkill() {
        return IsSilenced()
               || IsStunned()
               || IsKnockup();
    }

    public Buff GetBuffById(int id)
    {
        return buffList.FirstOrDefault(o => o.cfg.buffId == id);
    }
    #endregion

    #region LogicTimer
    
    public void CreateLogicTimer(Action cb,PEInt delayTime,int loopTime = 0)
    {
        LogicTimer timer = new LogicTimer(cb, delayTime,loopTime);
        timerList.Add(timer);
    }

    public void ClearFreeAniCallBack() {
        for(int i = 0; i < skillArr.Length; i++) {
            skillArr[i].FreeAniCallback = null;
        }
    }
    #endregion
}
