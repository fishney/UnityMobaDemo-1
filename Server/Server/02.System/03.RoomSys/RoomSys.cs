using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
	public class RoomSys : SystemBase<RoomSys>
    {
        private List<PVPRoom> pvpRoomList = null;
        private RoomSys() { }
        private object pvpRoomListLock = new object();

		public override void Init()
		{
			base.Init();
            pvpRoomList = new List<PVPRoom>();

            TimerSvc.Instance().AddTask(5000, CheckStatus, null, 0);

        }

		void CheckStatus(int id)
		{
			this.ColorLog(PEUtils.LogColor.Magenta, $"对战房间负载：{pvpRoomList.Count}个");
		}

        public void AddPvpRoom(ServerSession[] sessions,PvpEnum pvpType)
        {
            int roomId = GetRoomId();
            PVPRoom room = new PVPRoom(roomId,pvpType,sessions);
            pvpRoomList.Add(room);
        }

		public override void Update()
		{
			base.Update();
		}

        private int roomId = 0;
        public int GetRoomId()
        {
            return roomId++;
        }

        public void SendConfirm(MsgPack msgPack)
        {
	        SendConfirm req = msgPack.msg.sendConfirm;
	        var room = pvpRoomList.Find(o => o.roomId == req.roomId);

            if (room != null)
            {
	            room.SendConfirm(msgPack.session);
            }

        }

        public void SendSelect(MsgPack msgPack)
        {
            SendSelect req = msgPack.msg.sendSelect;
            var room = pvpRoomList.Find(o => o.roomId == req.roomId);

            if (room != null)
            {
                room.SendSelect(msgPack.session,req.heroId);
            }

        }

        public void SendLoadPrg(MsgPack msgPack)
        {
            SendLoadPrg req = msgPack.msg.sendLoadPrg;

            var room = pvpRoomList.Find(o => o.roomId == req.roomId);

            if (room != null)
            {
                room.SendLoadPrg(msgPack.session, req.percent);
            }

        }

        public void ReqBattleStart(MsgPack msgPack)
        {
            ReqBattleStart req = msgPack.msg.reqBattleStart;

            var room = pvpRoomList.Find(o => o.roomId == req.roomId);

            if (room != null)
            {
                room.ReqBattleStart(msgPack.session);
            }

        }

        public void SendOpKey(MsgPack msgPack)
        {
            SendOpKey req = msgPack.msg.sendOpKey;

            var room = pvpRoomList.Find(o => o.roomId == req.roomId);

            if (room != null)
            {
                room.SendOpKey(req.opKey);
            }
            else
            {
                this.Warn(req.roomId + " PVPRoom is not existed.");
            }
        }

        public void SendChat(MsgPack msgPack)
        {
	        SendChat req = msgPack.msg.sendChat;

	        var room = pvpRoomList.Find(o => o.roomId == req.roomId);

	        if (room != null)
	        {
		        room.SendChat(req.chatMsg);
	        }
	        else
	        {
		        this.Warn(req.roomId + " PVPRoom is not existed.");
	        }
        }

        public void ReqBattleEnd(MsgPack msgPack)
        {
	        ReqBattleEnd req = msgPack.msg.reqBattleEnd;

	        var room = pvpRoomList.Find(o => o.roomId == req.roomId);

	        if (room != null)
	        {
		        room.ReqBattleEnd(msgPack.session);
	        }

        }

        public void DestroyRoom(int roomId)
        {
	        lock (pvpRoomListLock)
	        {
		        var roomIndex = pvpRoomList.FindIndex(o => o.roomId == roomId);

		        if (roomIndex >= 0)
		        {
			        pvpRoomList.RemoveAt(roomIndex);

		        }
		        else
		        {
			        this.Error("PVPRoom is not exist ID:" + roomId);
		        }
            }
        }
    }
}