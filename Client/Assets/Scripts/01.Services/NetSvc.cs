/****************************************************
    文件：NetSvc.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using HOKProtocol;
using PENet;
using PEUtils;

public class NetSvc : GameRootMonoSingleton<NetSvc>
{
    private KCPNet<ClientSession, HokMsg> client = null;
    private Queue<HokMsg> msgQue = null;
    private static readonly string obj = "lock";
    private Task<bool> checkTask = null;
    
    public void InitSvc()
    {
        client = new KCPNet<ClientSession, HokMsg>();
        msgQue = new Queue<HokMsg>();
        
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
    
    public void SendMsg(HokMsg msg)
    {
        if (client.clientSession != null)
        {
            client.clientSession.SendMsg(msg);
        }
        else
        {
            GameRootResources.Instance().ShowTips("服务器未连接");
            InitSvc();
        }
    }

    /// <summary>
    /// 向客户端发送请求
    /// </summary>
    public void AddNetMsg(HokMsg msg)
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
    
    private void HandleRsp(HokMsg msg)
    {
       
    }
}
