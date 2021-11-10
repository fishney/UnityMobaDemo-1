using System;
using System.Collections.Generic;
using System.Net.Sockets.Kcp;
using System.Text;
using HOKProtocol;
using PENet;
using PEUtils;

namespace Server
{
	public class NetSvc : ServiceBase<NetSvc>
	{
		private NetSvc() { }
		private KCPNet<ServerSession, GameMsg> server = null;
		private Queue<MsgPack> msgPackQue = null;
		private static readonly string obj = "lock";

		public override void Init()
		{
			base.Init();
			server = new KCPNet<ServerSession, GameMsg>();
			msgPackQue = new Queue<MsgPack>();

			// 配置Log = 委托
			KCPTool.LogFunc = this.Log;
			KCPTool.WarnFunc = this.Warn;
			KCPTool.ErrorFunc = this.Error;
			KCPTool.ColorLogFunc = (color,msg) =>
			{
				this.ColorLog((LogColor)color,msg);
			};

			//启动
			server.StartAsServer(ServerConfig.LocalDevInnerIp, ServerConfig.UdpPort);
			this.Log("ServerSession init Completed by NetSvc.");

		}

		public override void Update()
		{
			base.Update();

			if (msgPackQue?.Count > 0)
			{
				this.Log("PacCount:" + msgPackQue?.Count);
				lock (obj)
				{
					MsgPack msg = msgPackQue.Dequeue();
					HandOutMsg(msg);
				}
			}
		}

		public void AddMsgQue(ServerSession session, GameMsg msg)
		{
			lock (obj)
			{
				msgPackQue.Enqueue(new MsgPack(session, msg));
			}
		}

		private void HandOutMsg(MsgPack msgPack)
		{
            switch ((CMD)msgPack.msg.cmd)
            {
                case CMD.ReqLogin:
                    LoginSys.Instance().ReqLogin(msgPack);
                    break;
                case CMD.ReqMatch:
                    MatchSys.Instance().ReqMatch(msgPack);
                    break;
                //case CMD.ReqGuide:
                //    GuideSys.Instance().ReqGuide(msgPack);
                //    break;
                //case CMD.ReqStrong:
                //    StrongSys.Instance().ReqStrong(msgPack);
                //    break;
                //case CMD.SendChat:
                //    ChatSys.Instance().SendChat(msgPack);
                //    break;
                //case CMD.ReqBuy:
                //    BuySys.Instance().ReqBuy(msgPack);
                //    break;
                //case CMD.ReqTask:
                //    TaskSys.Instance().ReqTask(msgPack);
                //    break;
                //case CMD.ReqDungeon:
                //    DungeonSys.Instance().ReqDungeon(msgPack);
                //    break;
                //case CMD.ReqDungeonEnd:
                //    DungeonSys.Instance().ReqDungeonEnd(msgPack);
                //    break;

            }
        }

	}
}
