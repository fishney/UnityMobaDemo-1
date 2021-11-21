/****************************************************
    文件：MainLogicUnit.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class MainLogicUnit : LogicUnit
{
    public override void LogicInit()
    {
        // 初始化属性
        InitProperties();
        // 初始化技能
        InitSkill();
        // 初始化移动控制
        InitMove();
    }

    public override void LogicTick()
    {
        TickSkill();
        TickMove();
    }

    public override void LogicUnInit()
    {
        UnInitSkill();
        UnInitMove();
    }
}
