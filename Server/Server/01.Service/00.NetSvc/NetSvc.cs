using System;
using System.Collections.Generic;
using System.Text;
using CodingK_Session;
using proto.HOKProtocol;
using PEUtils;

namespace Server
{
	public class NetSvc : ServiceBase<NetSvc>
	{
		private NetSvc() { }
		private CodingK_Net<ServerSession, GameMsg> server = null;
		private Queue<MsgPack> msgPackQue = null;
		private static readonly string obj = "lock";

		public override void Init()
		{
			base.Init();
			server = new CodingK_Net<ServerSession, GameMsg>();
			msgPackQue = new Queue<MsgPack>();

			// 配置Log = 委托
            CodingK_SessionTool.LogFunc = this.Log;
            CodingK_SessionTool.WarnFunc = this.Warn;
            CodingK_SessionTool.ErrorFunc = this.Error;
            CodingK_SessionTool.ColorLogFunc = (color,msg) =>
			{
				this.ColorLog((LogColor)color,msg);
			};

			//启动
//#if DEBUG
			server.StartAsServer(ServerConfig.LocalDevInnerIp, ServerConfig.UdpPort, ServerRoot.Instance().protocolMode);
// #else
//             server.StartAsServer(ServerConfig.RemoteServerIp, ServerConfig.UdpPort);
// #endif

			this.Log("ServerSession init Completed by NetSvc.");

		}

		public override void Update()
		{
			base.Update();

			while (msgPackQue?.Count > 0)
			{
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
                case CMD.ReqBagItem:
                    LobbySys.Instance().ReqBagItem(msgPack);
                    break;
				case CMD.ReqMatch:
                    MatchSys.Instance().ReqMatch(msgPack);
                    break;
                case CMD.SendConfirm:
	                RoomSys.Instance().SendConfirm(msgPack);
	                break;
                case CMD.SendSelect:
                    RoomSys.Instance().SendSelect(msgPack);
                    break;
                case CMD.SendLoadPrg:
                    RoomSys.Instance().SendLoadPrg(msgPack);
                    break;
                case CMD.ReqBattleStart:
                    RoomSys.Instance().ReqBattleStart(msgPack);
                    break;
				case CMD.SendOpKey:
                    RoomSys.Instance().SendOpKey(msgPack);
                    break;
				case CMD.SendChat:
					RoomSys.Instance().SendChat(msgPack);
					break;
				case CMD.ReqBattleEnd:
					RoomSys.Instance().ReqBattleEnd(msgPack);
					break;
				case CMD.ReqPing:
					SyncPingCMD(msgPack);
					break;
            }
        }

		private void SyncPingCMD(MsgPack pack)
		{
			ReqPing req = pack.msg.reqPing;
			GameMsg msg = new GameMsg
			{
				cmd = CMD.RspPing,
				rspPing = new RspPing
				{
					pingId = req.pingId,
				}
			};
			pack.session.SendMsg(msg);
		}

	}
}
