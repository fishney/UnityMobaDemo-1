/****************************************************
    文件：LobbySys.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using proto.HOKProtocol;
using UnityEngine;

public class LobbySys : SystemBase
{
    public static LobbySys Instance;

    public override void InitSys()
    {
        base.InitSys();
        
        Instance = this;
        this.Log("LobbySys Init Completed.");
    }

    public void EnterLobby()
    {
        resSvc.AsyncLoadScene("MainCity", null, null);
        gameRootResources.lobbyWindow.SetWindowState();
    }

    public void RspMatch(GameMsg msg)
    {
        int preTime = msg.rspMatch.preTime;
        gameRootResources.lobbyWindow.ShowMatchInfo(true,preTime);
    }
    
    public void NotifyConfirm(GameMsg msg)
    {
        NotifyConfirm notify = msg.notifyConfirm;
        
        if (notify.dismiss)
        {
            // 匹配对局被某人拒绝了
            gameRootResources.matchWindow.SetWindowState(false);
            gameRootResources.lobbyWindow.SetWindowState();
        }
        else
        {
            GameRoot.ActiveRoomId = notify.roomId;
            
            gameRootResources.lobbyWindow.SetWindowState(false);
            // 只有第一次是没打开的,后续9次都不用打开
            if (gameRootResources.matchWindow.gameObject.activeSelf == false)
            {
                gameRootResources.matchWindow.SetWindowState();
            }
            gameRootResources.matchWindow.RefreshUI(notify.confirmArr);
        }
        
        
    }
    
    public void NotifySelect(GameMsg msg)
    {
        gameRootResources.matchWindow.SetWindowState(false);
        gameRootResources.selectWindow.SetWindowState(true);
    }
    
    public void NotifyLoadRes(GameMsg msg)
    {
        NotifyLoadRes data = msg.notifyLoadRes;
        GameRoot.MapId = data.mapId;
        GameRoot.SelfPosIndex = data.posIndex;
        GameRoot.battleHeroList = data.heroList;
        
        gameRootResources.selectWindow.SetWindowState(false);
        // 流程转入战斗系统
        BattleSys.Instance.EnterBattle();
    }

    public void RspUseItem(GameMsg msg)
    {
        var updatedPlayerData = msg.rspBagItem.updatedPlayerData;
        GameRoot.Instance().SetPlayerData(updatedPlayerData,true);
        
        var usedItemId = msg.rspBagItem.usedItem;
        if (gameRootResources.bagWindow.gameObject.activeSelf)
        {
            gameRootResources.bagWindow.RspUseItem(usedItemId, true);
        }
    }
    
}
