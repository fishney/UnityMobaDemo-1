/****************************************************
    文件：PlayWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using PEMath;
using UnityEngine;

public class PlayWindow : WindowBase
{
    protected override void InitWindow()
    {
        base.InitWindow();
    }
    
    protected override void ClearWindow()
    {
        base.ClearWindow();
    }

    private Vector2 lastKeyDir = Vector2.zero;
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 keyDir = new Vector2(h, v);
        if (keyDir != lastKeyDir)
        {
            if (h != 0 || v != 0)
            {
                keyDir = keyDir.normalized;
            }
            InputMoveKey(keyDir);
            lastKeyDir = keyDir;
        }
    }

    private Vector2 lastStickDir = Vector2.zero;
    private void InputMoveKey(Vector2 dir)
    {
        if (!dir.Equals(lastStickDir))
        {
            // 适配相机，旋转角度45，使角色移动方向与地图的视觉统一
            Vector3 dirVector3 = new Vector3(dir.x, 0, dir.y);
            dirVector3 = Quaternion.Euler(0, 45, 0) * dirVector3;
            
            PEVector3 logicDir = PEVector3.zero;
            if (dir != Vector2.zero)
            {
                logicDir.x = (PEInt) dirVector3.x;
                logicDir.y = (PEInt) dirVector3.y;
                logicDir.z = (PEInt) dirVector3.z;
            }

            bool isSend = BattleSys.Instance.SendMoveKey(logicDir);
            if (isSend)
            {
                lastStickDir = dir;
            }
        }
    }
        
}
