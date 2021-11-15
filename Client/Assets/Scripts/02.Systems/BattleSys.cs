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
using HOKProtocol;
using UnityEngine;

public class BattleSys : SystemBase
{
    public static BattleSys Instance;

    private int mapId;
    
    public override void InitSys()
    {
        base.InitSys();
        
        Instance = this;
        this.Log("BattleSys Init Completed.");
    }

    public void EnterBattle()
    {
        audioSvc.StopBGM();
        gameRootResources.loadWindow.SetWindowState(true);

        mapId = GameRoot.MapId;
    }

    private int lastPercent = 0;
    void SceneLoadProgress(float val)
    {
        int percent = (int) (val * 100);
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.SendLoadPrg,
            sendLoadPrg = new SendLoadPrg()
            {
                percent = percent,
                roomId = GameRoot.ActiveRoomId,
            }
        };
        netSvc.SendMsg(msg);
        lastPercent = percent;
        
    }

    void SceneLoadDone()
    {
        
    }
    
}
