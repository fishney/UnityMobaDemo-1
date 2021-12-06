/****************************************************
    文件：MainLogicSkill.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

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
    
    void InitSkill()
    {
        int len = ud.unitCfg.skillArr.Length;
        skillArr = new Skill[len];
        for (int i = 0; i < len; i++)
        {
            skillArr[i] = new Skill(ud.unitCfg.skillArr[i],this);
        }
    }
    
    void TickSkill()
    {
        
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
}
