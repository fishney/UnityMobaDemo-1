

using System;
using HOKProtocol;
using PENet;

public class ClientSession: KCPSession<GameMsg>
{
    public int sessionId = 0;

    protected override void OnUpdate(DateTime now)
    {
			
    }

    protected override void OnConnected()
    {
        // GameRootResources.Instance().ShowTips("服务器连接成功");
        this.Log("SessionId:" + sessionId + " Client Connect");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        this.Log("SessionId:" + sessionId + " RcvPack CMD:");

        // 向消息队列添加新的消息处理，等待被轮询执行（执行线程不固定）
        NetSvc.Instance().AddNetMsg(msg);
    }

    protected override void OnDisConnected()
    {
        GameRootResources.Instance().ShowTips("服务器已断开连接");
        this.Log("SessionId:" + sessionId + " Client Offline");
    }


}
