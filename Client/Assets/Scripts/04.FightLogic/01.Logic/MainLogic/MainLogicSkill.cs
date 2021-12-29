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
using HOKProtocol;
using PEMath;

/// <summary>
/// MainLogicSkill
/// </summary>
public partial class MainLogicUnit
{
    protected Skill[] skillArr;
    private List<LogicTimer> timerList;
    
    void InitSkill()
    {
        int len = ud.unitCfg.skillArr.Length;
        skillArr = new Skill[len];
        timerList = new List<LogicTimer>();
        for (int i = 0; i < len; i++)
        {
            skillArr[i] = new Skill(ud.unitCfg.skillArr[i],this);
        }
    }
    
    void TickSkill()
    {
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

    #region API Func

    public Skill GetNormalSkill()
    {
        return skillArr[0];
    }

    #endregion

    #region LogicTimer
    
    public void CreateLogicTimer(Action cb,PEInt delayTime,int loopTime = 0)
    {
        LogicTimer timer = new LogicTimer(cb, delayTime,loopTime);
        timerList.Add(timer);
    }

    #endregion
}
