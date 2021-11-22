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
    
    
    void InitProperties()
    {
        Hp = ud.unitCfg.hp;
        Def = ud.unitCfg.def;
    }

}