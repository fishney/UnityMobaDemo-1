/****************************************************
    文件：HeroView.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：英雄表现控制
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能释放显示、旁白
/// 
/// </summary>
public class HeroView : MainViewUnit
{
    private Hero hero;

    public override void Init(LogicUnit logicUnit)
    {
        base.Init(logicUnit);
        
        hero = logicUnit as Hero;
    }
    
    protected override Vector3 GetUnitViewDir()
    {
        // 玩家朝向使用UI输入位置朝向，不使用物理引擎运算修正方向
        // 从而防止特殊情况，比如撞上墙会被物理引擎修正朝向
        return hero.InputDir.ConvertViewVector3();
    }
}
