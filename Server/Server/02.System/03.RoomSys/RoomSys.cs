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
        
    }
}
