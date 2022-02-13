using System;
using System.Collections.Generic;
using System.Text;
using proto.HOKProtocol;

namespace Server
{
    /// <summary>
    /// 战斗结束
    /// </summary>
    public class RoomStateEnd : RoomStateBase
    {
        public RoomStateEnd(PVPRoom r) : base(r)
        {
        }

        public override void Enter()
        {
	        GameMsg msg = new GameMsg
            {
		        cmd = CMD.RspBattleEnd,
		        rspBattleEnd = new RspBattleEnd
		        {
			        // TODO 结算数据
		        }
	        };

	        room.PublishMsg(msg);
	        Exit();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
	        RoomSys.Instance().DestroyRoom(room.roomId);
        }
    }
}
