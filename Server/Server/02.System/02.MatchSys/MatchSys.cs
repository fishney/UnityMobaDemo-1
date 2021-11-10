using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
	public class MatchSys : SystemBase<MatchSys>
    {
        private Queue<ServerSession> que1V1;
        private Queue<ServerSession> que2V2;
        private Queue<ServerSession> que5V5;


		private MatchSys() { }

		public override void Init()
		{
			base.Init();

            que1V1 = new Queue<ServerSession>();
            que2V2 = new Queue<ServerSession>();
            que5V5 = new Queue<ServerSession>();

        }

		public override void Update()
		{
			base.Update();
		}

        /// <summary>
        /// 整体逻辑是,3个队列对应3种不同人数的匹配模式,匹配满人了就开。
        /// </summary>
        /// <param name="msgPack"></param>
        public void ReqMatch(MsgPack msgPack)
        {
            ReqMatch data = msgPack.msg.reqMatch;
            PvpEnum pvpEnum = data.pvpEnum;

            switch (pvpEnum)
            {
                case PvpEnum._1V1:
                    que1V1.Enqueue(msgPack.session);
                    break;
                case PvpEnum._2V2:
                    que2V2.Enqueue(msgPack.session);
                    break;
                case PvpEnum._5V5:
                    que5V5.Enqueue(msgPack.session);
                    break;
                default:
                    this.Error("PVPType Error:" + pvpEnum.ToString());
                    break;
            }

            GameMsg msg = new GameMsg
            {
                cmd = CMD.RspMatch,
                rspMatch = new RspMatch
                {
                    preTime = GetPreTime(pvpEnum),
                }
            };

            msgPack.session.SendMsg(msg);
        }

        private int GetPreTime(PvpEnum pvpEnum)
        {
            /// 开启游戏还差的人数
            int waitCount = 0;
            switch (pvpEnum)
            {
                case PvpEnum._1V1:
                    waitCount = 2 - que1V1.Count;
                    break;
                case PvpEnum._2V2:
                    waitCount = 4 - que2V2.Count;
                    break;
                case PvpEnum._5V5:
                    waitCount = 10 - que5V5.Count;
                    break;
            }

            if (waitCount < 0)
            {
                waitCount = 0;
            }

            // 简单算一下，返回
            return waitCount * 10 + 5;
        }
	}
}
