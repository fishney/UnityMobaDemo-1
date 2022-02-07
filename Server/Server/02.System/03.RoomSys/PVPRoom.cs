using System;
using System.Collections.Generic;
using System.Text;
using CodingK_Session;
using HOKProtocol;

namespace Server
{
    public class PVPRoom
    {
        public int roomId;
        public PvpEnum pvpType = PvpEnum.None;
        public List<ServerSession> sessionArr;

        private RoomStateEnum currentState = RoomStateEnum.None;
        private Dictionary<RoomStateEnum, RoomStateBase> fsm = new Dictionary<RoomStateEnum, RoomStateBase>();

        /// 存储房间内选择的英雄数据,从Select状态传入,在Fight使用
        private SelectData[] selectArr = null;
        /// 存储房间内选择的英雄数据,从Select状态传入,在Fight使用
        public SelectData[] SelectArr
        {
            get
            {
                return selectArr;
            }
            set
            {
                selectArr = value;
            }
        }

        public PVPRoom(int roomId,PvpEnum pvpType,ServerSession[] sessions)
        {
            this.roomId = roomId;
            this.pvpType = pvpType;
            this.sessionArr = new List<ServerSession>();
            sessionArr.AddRange(sessions);

            fsm.Add(RoomStateEnum.Confirm, new RoomStateConfirm(this));
            fsm.Add(RoomStateEnum.Select, new RoomStateSelect(this));
            fsm.Add(RoomStateEnum.Load, new RoomStateLoad(this));
            fsm.Add(RoomStateEnum.Fight, new RoomStateFight(this));
            fsm.Add(RoomStateEnum.End, new RoomStateEnd(this));

            ChangeRoomState(RoomStateEnum.Confirm);
        }

        public void ChangeRoomState(RoomStateEnum targetState)
        {
            if (currentState == targetState)
            {
                return;
            }

            var hasNext = fsm.TryGetValue(targetState,out var next);
            if (hasNext)
            {
                if (currentState != RoomStateEnum.None)
                {
                    fsm[currentState].Exit();
                }

                next.Enter();
                currentState = targetState;
            }
        }

        /// <summary>
        /// 对全房间人全部推送
        /// </summary>
        /// <param name="msg"></param>
        public void PublishMsg(GameMsg msg)
        {
            byte[] bytes;
            if (ServerRoot.Instance().protocolMode == CodingK_ProtocolMode.Proto)
            {
                bytes = CodingK_SessionTool.ProtoSerialize(msg);
            }
            else
            {
                bytes = CodingK_SessionTool.Serialize(msg);
            }
                

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

        public void SendSelect(ServerSession session,int heroId)
        {
            if (currentState == RoomStateEnum.Select)
            {
                if (fsm[currentState] is RoomStateSelect state)
                {
                    state.UpdateHeroSelect(GetPosIndex(session),heroId);

                }
            }
        }

        public void SendLoadPrg(ServerSession session, int percent)
        {
            if (currentState == RoomStateEnum.Load)
            {
                if (fsm[currentState] is RoomStateLoad state)
                {
                    state.UpdateLoadState(GetPosIndex(session), percent);
                }
            }
        }

        public void ReqBattleStart(ServerSession session)
        {
            if (currentState == RoomStateEnum.Load)
            {
                if (fsm[currentState] is RoomStateLoad state)
                {
                    state.UpdateLoadDone(GetPosIndex(session));
                }
            }
        }

        public void SendOpKey(OpKey key)
        {
            if (currentState == RoomStateEnum.Fight)
            {
                if (fsm[currentState] is RoomStateFight state)
                {
                    state.UpdateOpKey(key);
                }
            }
        }

        public void SendChat(string chatMsg)
        {
	        GameMsg msg = new GameMsg
	        {
		        cmd = CMD.NotifyChat,
		        notifyChat = new NotifyChat
		        {
			        chatMsg = chatMsg,
		        }
	        };

	        PublishMsg(msg);
        }

        public void ReqBattleEnd(ServerSession session)
        {
	        if (currentState == RoomStateEnum.Fight)
	        {
		        if (fsm[currentState] is RoomStateFight state)
		        {
			        state.UpdateEndState(GetPosIndex(session));
		        }
	        }
        }

        public void Clear()
        {
            selectArr = null;
            sessionArr = null;
            fsm = null;
        }
    }
}
