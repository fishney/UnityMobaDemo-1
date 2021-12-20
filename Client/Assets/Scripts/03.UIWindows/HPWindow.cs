/****************************************************
    文件：HPWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPWindow : WindowBase
{
    public Transform hpItemRoot;// 血条
    public Transform jumpNumRoot;// 伤害跳字
    public int jumpNumCount;// 缓存池数量
    
    protected override void InitWindow()
    {
        base.InitWindow();
    }
    
    protected override void ClearWindow()
    {
        base.ClearWindow();
    }
}
