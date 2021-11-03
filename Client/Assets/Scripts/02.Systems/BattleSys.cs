/****************************************************
    文件：BattleSys.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SystemBase
{
    public static BattleSys Instance;

    public void InitSys()
    {
        base.InitSys();
        
        Instance = this;
        this.Log("BattleSys Init Completed.");
    }
}
