/****************************************************
    文件：NetSvc.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using HOKProtocol;
using PENet;
using PEUtils;

public class NetSvc : GameRootMonoSingleton<NetSvc>
{
    private KCPNet<ClientSession, GameMsg> client = null;
    private Queue<GameMsg> msgQue = null;
    private static readonly string obj = "lock";
    private Task<bool> checkTask = null;
    
    public void InitSvc()
    {
        client = new KCPNet<ClientSession, GameMsg>();
        msgQue = new Queue<GameMsg>();
        
        KCPTool.LogFunc = this.Log;
        KCPTool.WarnFunc = this.Warn;
        KCPTool.ErrorFunc = this.Error;
        KCPTool.ColorLogFunc = (color,msg) =>
        {
            this.ColorLog((LogColor)color,msg);
        };

        string svcIP = ServerConfig.LocalDevInnerIp;
        if (GameRootResources.Instance().loginWindow != null)
        {
            if (GameRootResources.Instance().loginWindow.togServer.isOn)
            {
                // TODO 公网IP svcIP
            }
        }

        //启动
        client.StartAsClient(svcIP, ServerConfig.UdpPort);
        // 检测成功间隔
        checkTask = client.ConnectServer(100);
        Debug.Log("NetSvc Init Completed.");
    }
    
    public void SendMsg(GameMsg msg,Action<bool> cb = null)
    {
        if (client.clientSession != null)
        {
            client.clientSession.SendMsg(msg);
            cb?.Invoke(true);
        }
        else
        {
            GameRootResources.Instance().ShowTips("服务器未连接");
            InitSvc();
            cb?.Invoke(false);
        }
    }

    /// <summary>
    /// 向客户端发送请求
    /// </summary>
    public void AddNetMsg(GameMsg msg)
    {
        lock (obj)
        {
            msgQue.Enqueue(msg);
        }
    }

    private int counter = 0;
    public void Update()
    {
        if (checkTask!= null && checkTask.IsCompleted)
        {
            if (checkTask.Result)
            {
                this.Log("ConnectServer Success.");
                checkTask = null;
                GameRootResources.Instance().ShowTips("服务器连接成功");
                // TODO send ping msg
            }
            else
            {
                ++counter;
                if (counter>4)
                {
                    this.Error($"ConnectServer Failed{counter}.Please Check.");
                    GameRootResources.Instance().ShowTips("服务器连接失败次数过多，请检查网络");
                    checkTask = null;
                }
                else
                {
                    this.Warn($"ConnectServer Failed{counter}.Retry Now.");
                    checkTask = client.ConnectServer(100);
                }
            }
        }

        if (client != null && client.clientSession != null)
        {
            if (msgQue?.Count > 0)
            {
                lock (obj)
                {
                    var msg = msgQue.Dequeue();
                    HandleRsp(msg);
                }
            }
        }
        
    }
    
    private void HandleRsp(GameMsg msg)
    {
       if (msg.err != (int)ErrorCode.None)
        {
            switch ((ErrorCode)msg.err)
            {
                case ErrorCode.AccountIsOnline :
                    GameRootResources.Instance().ShowTips("当前账号已在线！");
                    break;
                case ErrorCode.WrongPass :
                    GameRootResources.Instance().ShowTips("输入账户名或密码错误！");
                    break;
                case ErrorCode.ServerDataError :
                    this.Log("服务端数据异常",LogType.Error);
                    GameRootResources.Instance().ShowTips("服务端数据异常！");
                    break;
                case ErrorCode.ClientDataError :
                    this.Log("客户端数据异常",LogType.Error);
                    GameRootResources.Instance().ShowTips("客户端数据异常！");
                    break;
                case ErrorCode.UpdateDBError :
                    this.Log("数据库更新异常",LogType.Error);
                    GameRootResources.Instance().ShowTips("网络不稳定！");
                    break;
                case ErrorCode.LackLevel :
                    this.Log("作弊检测：等级不足",LogType.Error);
                    GameRootResources.Instance().ShowTips("等级不足");
                    break;
                case ErrorCode.LackCoin :
                    this.Log("作弊检测：金币不足",LogType.Error);
                    GameRootResources.Instance().ShowTips("金币不足");
                    break;
                case ErrorCode.LackCrystal :
                    this.Log("作弊检测：晶体不足",LogType.Error);
                    GameRootResources.Instance().ShowTips("晶体不足");
                    break;
                case ErrorCode.LackDiamond :
                    this.Log("作弊检测：钻石不足",LogType.Error);
                    GameRootResources.Instance().ShowTips("钻石不足");
                    break;
                case ErrorCode.LackPower :
                    this.Log("作弊检测：体力不足",LogType.Error);
                    GameRootResources.Instance().ShowTips("体力不足");
                    break;
            }
            
            return;
        }
        
        switch ((CMD)msg.cmd)
        {
            case CMD.RspLogin:
                LoginSys.Instance.RspLogin(msg);
                break;
            case CMD.RspMatch:
                LobbySys.Instance.RspMatch(msg);
                break;
            case CMD.NotifyConfirm:
                LobbySys.Instance.NotifyConfirm(msg);
                break;
                
            // case CMD.RspRename:
            //     LoginSys.Instance.RspRename(msg);
            //     break;
            // case CMD.RspGuide:
            //     MainCitySys.Instance.RspGuide(msg);
            //     break;
            // case CMD.RspStrong:
            //     MainCitySys.Instance.RspStrong(msg);
            //     break;
            // case CMD.PushChat:
            //     MainCitySys.Instance.PushChat(msg);
            //     break;
            // case CMD.RspBuy:
            //     MainCitySys.Instance.RspBuy(msg);
            //     break;
            // case CMD.PushPower:
            //     MainCitySys.Instance.PushPower(msg);
            //     // MainCitySys.Instance.PushTaskPrgs(msg); TODO 如果想并包，两次请求操作合成一次，可以这样
            //     break;
            // case CMD.RspTask:
            //     MainCitySys.Instance.RspTask(msg);
            //     break;
            // case CMD.PushTaskPrgs:
            //     MainCitySys.Instance.PushTaskPrgs(msg);
            //     break;
            // case CMD.RspDungeon:
            //     DungeonSys.Instance.RspDungeon(msg);
            //     break;
            // case CMD.RspDungeonEnd:
            //     BattleSys.Instance.RspDungeonEnd(msg);
            //     break;
        }
    }
}
