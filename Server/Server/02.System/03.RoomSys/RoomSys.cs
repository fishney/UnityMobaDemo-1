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

		public override void Init()
		{
			base.Init();
            pvpRoomList = new List<PVPRoom>();
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
        
    }
}
