

using System;
using proto.HOKProtocol;
using CodingK_Session;


public class ClientSession: CodingK_Session<GameMsg>
{
    public int sessionId = 0;

    protected override void OnUpDate(DateTime now)
    {
			
    }

    protected override void OnConnected()
    {
        GameRootResources.Instance().ShowTips("服务器连接成功");
        this.Log("SessionId:" + sessionId + " Client Connect");
    }

    protected override void OnReceiveMsg(GameMsg msg)
    {
        // this.Log("SessionId:" + sessionId + " RcvPack CMD:");

        // 向消息队列添加新的消息处理，等待被轮询执行（执行线程不固定）
        NetSvc.Instance().AddNetMsg(msg);
    }

    protected override void OnDisConnected()
    {
        GameRootResources.Instance().ShowTips("服务器已断开连接");
        this.Log("SessionId:" + sessionId + " Client Offline");
    }


}
