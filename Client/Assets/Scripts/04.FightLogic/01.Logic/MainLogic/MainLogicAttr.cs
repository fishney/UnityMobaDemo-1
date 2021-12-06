/****************************************************
    文件：MainLogicAttr.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using PEMath;

/// <summary>
/// MainLogicAttributes
/// </summary>
public partial class MainLogicUnit
{

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

}