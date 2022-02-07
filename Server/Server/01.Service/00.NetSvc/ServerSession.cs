using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;
using CodingK_Session;

namespace Server
{
	public class ServerSession : CodingK_Session<GameMsg>
	{
		public int sessionId = 0;

		protected override void OnUpDate(DateTime now)
		{
			
		}

		protected override void OnConnected()
		{
			sessionId = ServerRoot.Instance().GetSessionId();
			this.Log("SessionId:" + sessionId + " Client Connect" + m_sessionId);
		}

		protected override void OnReceiveMsg(GameMsg msg)
		{
			this.Log("SessionId:" + sessionId + " RcvPack CMD:");

			// 向消息队列添加新的消息处理，等待被轮询执行（执行线程不固定）
			NetSvc.Instance().AddMsgQue(this, msg);
		}

		protected override void OnDisConnected()
		{
			LoginSys.Instance().ClearOfflineData(this);
			this.Log("SessionId:" + sessionId + " Client Offline");
		}


    }
}
