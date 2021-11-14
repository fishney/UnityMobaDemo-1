using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;
using PENet;

namespace Server
{
    public class PVPRoom
    {
        public int roomId;
        public PvpEnum pvpType = PvpEnum.None;
        public List<ServerSession> sessionArr;

        private RoomStateEnum currentState = RoomStateEnum.None;
        private Dictionary<RoomStateEnum, RoomStateBase> fsm = new Dictionary<RoomStateEnum, RoomStateBase>();

        public PVPRoom(int roomId,PvpEnum pvpType,ServerSession[] sessions)
        {
            this.roomId = roomId;
            this.pvpType = pvpType;
            this.sessionArr = new List<ServerSession>();
            sessionArr.AddRange(sessions);

            fsm.Add(RoomStateEnum.Confirm, new RoomStateConfirm(this));
            fsm.Add(RoomStateEnum.Select, new RoomStateConfirm(this));
            fsm.Add(RoomStateEnum.Load, new RoomStateConfirm(this));
            fsm.Add(RoomStateEnum.Fight, new RoomStateConfirm(this));
            fsm.Add(RoomStateEnum.End, new RoomStateConfirm(this));

            ChangeRoomState(RoomStateEnum.Confirm);
        }

        public void ChangeRoomState(RoomStateEnum targetState)
        {
            if (currentState == targetState)
            {
                return;
            }

            if (fsm.ContainsKey(targetState))
            {
                if (currentState != RoomStateEnum.None)
                {
                    fsm[currentState].Exit();
                }

                fsm[targetState].Enter();
                currentState = targetState;
            }
        }

        /// <summary>
        /// 对全房间人全部推送
        /// </summary>
        /// <param name="msg"></param>
        public void PublishMsg(GameMsg msg)
        {
            byte[] bytes = KCPTool.Serialize(msg);

            foreach (var session in sessionArr)
            {
                // 这里如果传msg,底层传一次会序列化一次,所以优化一下,换成序列化一次直接传bytes
                session.SendMsg(bytes);
            }
        }

        public void SendConfirm(ServerSession session)
        {
	        if (currentState == RoomStateEnum.Confirm)
	        {
		        if (fsm[currentState] is RoomStateConfirm state)
		        {
			        state.UpdateConfirmState(GetPosIndex(session));

		        }
	        }
        }

        // 根据session获取玩家Match画面的相对位置
        private int GetPosIndex(ServerSession session)
        {
	        return sessionArr.IndexOf(session);
        }
    }

}
