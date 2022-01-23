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

        string svcIP = ServerConfig.RemoteGateIp;
        if (GameRootResources.Instance().loginWindow != null)
        {
            if (! GameRootResources.Instance().loginWindow.togServer.isOn)
            {
                svcIP = ServerConfig.LocalDevInnerIp;
            }
        }

        this.ColorLog(LogColor.Green, "ServerIP:" + svcIP);
        CancelInvoke("NetPing");
        //启动
        client.StartAsClient(svcIP, ServerConfig.UdpPort);
        // 检测成功间隔
        checkTask = client.ConnectServer(100);
        Debug.Log("NetSvc Init Completed.");
    }
    
    public void SendMsg(GameMsg msg,Action<bool> cb = null)
    {
        // 模拟服务器接收到了信息
        if (GMSystem.Instance.isActive)
        {
            GMSystem.Instance.SimulateServerRcvMsg(msg);
            cb?.Invoke(true);
            return;
        }
        
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
                
                // method name, delay time (second), interval time (second)
                InvokeRepeating("NetPing", 5, 5);
            }
            else
            {
                ++counter;
                if (counter > 4)
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
            while (msgQue?.Count > 0)
            {
                lock (obj)
                {
                    var msg = msgQue.Dequeue();
                    HandleRsp(msg);
                }
            }
        }

        // 模拟收到服务器消息
        if (GMSystem.Instance.isActive)
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
    
    uint sendPingId = 0;
    int pingCounter = 0;
    Dictionary<uint, DateTime> pingDic = new Dictionary<uint, DateTime>();

    public void NetPing() {
        ++sendPingId;
        SendMsg(new GameMsg() {
            cmd = CMD.ReqPing,
            reqPing = new ReqPing {
                pingId = sendPingId,
                sendTime = KCPTool.GetUTCStartMilliseconds(),
            }
        });

        //检测Ping有没有回应，累计三次没有回应，弹出提示
        if(pingDic.Count > 0) {
            ++pingCounter;
            if(pingCounter >= 3) {
                GameRootResources.Instance().ShowTips("网络异常，检测手机网络环境");
                pingCounter = 0;
            }
        }
        pingDic.Add(sendPingId, DateTime.Now);
    }
    
    void RspPing(GameMsg msg) {
        RspPing rsp = msg.rspPing;
        
        this.Log("Get pingId:" + rsp + ",count:" + pingDic.Count);
        
        uint recivePingID = rsp.pingId;
        if(pingDic.ContainsKey(recivePingID)) {
            TimeSpan ts = DateTime.Now - pingDic[recivePingID];
            GameRoot.NetDelay = (int)ts.TotalMilliseconds;
            pingDic.Clear();
            pingCounter = 0;
        }
        else {
            this.Warn("Net Ping ID Error:" + recivePingID);
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
            case CMD.NotifySelect:
                LobbySys.Instance.NotifySelect(msg);
                break;
            case CMD.NotifyLoadRes:
                LobbySys.Instance.NotifyLoadRes(msg);
                break;
            case CMD.NotifyLoadPrg:
                BattleSys.Instance.NotifyLoadPrg(msg);
                break;
            case CMD.RspBattleStart:
                BattleSys.Instance.RspBattleStart(msg);
                break;
            case CMD.NotifyOpKey:
                BattleSys.Instance.NotifyOpKey(msg);
                break;
            case CMD.NotifyChat:
                BattleSys.Instance.NotifyChat(msg);
                break;
            case CMD.RspBattleEnd:
                BattleSys.Instance.RspBattleEnd(msg);
                break;
            case CMD.RspPing:
                RspPing(msg);
                break;
    
        }
    }
}
