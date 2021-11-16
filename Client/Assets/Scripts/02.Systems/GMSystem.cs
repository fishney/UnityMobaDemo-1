/****************************************************
    文件：GMSystem.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：模拟服务器 测试用Sys
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using UnityEngine;

public class GMSystem : SystemBase
{
    public static GMSystem Instance;

    public override void InitSys()
    {
        base.InitSys();
        
        Instance = this;
        this.Log("GMSystem Init Completed.");
    }

    public void StartSimulate()
    {
        StartCoroutine(BattleSimulate());
    }

    public IEnumerator BattleSimulate()
    {
        SimulateLoadRes();
        yield return new WaitForSeconds(0.5f);
        SimulateBattleStart();
    }

    void SimulateLoadRes()
    {
        var msg = new GameMsg
        {
            cmd = CMD.NotifyLoadRes,
            notifyLoadRes = new NotifyLoadRes()
            {
                mapId = 102,
                heroList = new List<BattleHeroData>()
                {
                    new BattleHeroData() {heroId = 101, userName = "AAA"},
                    new BattleHeroData() {heroId = 102, userName = "BBB"},
                },
                posIndex = 0
            }
        };
        
        LobbySys.Instance.NotifyLoadRes(msg);
    }

    void SimulateBattleStart()
    {
        var msg = new GameMsg()
        {
            cmd = CMD.RspBattleStart
        };
        BattleSys.Instance.RspBattleStart(msg);
    }
}
