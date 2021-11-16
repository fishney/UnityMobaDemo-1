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
    private List<BattleHeroData> battleHeroList;
    private GameObject fightGO;
    private FightMgr fightMgr;
    private AudioSource battleAudio;
    
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
        battleHeroList = GameRoot.battleHeroList;
        resSvc.AsyncLoadScene("map_" + mapId,　SceneLoadProgress,　SceneLoadDone);
    }

    private int lastPercent = 0;
    void SceneLoadProgress(int percent)
    {
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
        // 初始化UI
        gameRootResources.playWindow.SetWindowState();
        
        // 加载角色资源
        
        
        //初始化战斗
        fightGO = new GameObject()
        {
            name = "fight"
        };
        fightMgr = fightGO.AddComponent<FightMgr>();
        battleAudio = fightGO.AddComponent<AudioSource>();
        var mapCfg = resSvc.GetMapConfigById(mapId);
        fightMgr.Init(battleHeroList,mapCfg);
        
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.ReqBattleStart,
            reqBattleStart = new ReqBattleStart()
            {
                roomId = GameRoot.ActiveRoomId,
            }
        };
        netSvc.SendMsg(msg);


    }

    public void NotifyLoadPrg(GameMsg msg)
    {
        gameRootResources.loadWindow.RefreshPrgData(msg.notifyLoadPrg.percentList);
    }

    public void RspBattleStart(GameMsg msg)
    {
        gameRootResources.loadWindow.SetWindowState(false);
        
        audioSvc.PlayBGMusic(Constants.BGBattle);
    }
}
