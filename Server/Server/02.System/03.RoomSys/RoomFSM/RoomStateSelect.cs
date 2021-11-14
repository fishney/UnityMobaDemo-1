using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
    /// <summary>
    /// 选择英雄
    /// </summary>
    public class RoomStateSelect : RoomStateBase
    {

	    /// <summary>
	    /// 房间内玩家信息,是否确认
	    /// </summary>
	    private SelectData[] selectArr = null;
	    private int checkTaskId;
	    private bool isAllSelected;

        public RoomStateSelect(PVPRoom r) : base(r)
        {
	        var num = r.sessionArr.Count;
	        selectArr = new SelectData[num];
        }

        public override void Enter()
        {
	        for (int i = 0; i < selectArr.Length; i++)
	        {
		        selectArr[i] = new SelectData
		        {
			        selectId = 0,
			        selectDone = false,
		        };
	        }

	        GameMsg msg = new GameMsg
	        {
		        cmd = CMD.NotifySelect,
	        };

	        room.PublishMsg(msg);
	        //checkTaskId = TimerSvc.Instance().AddTask(ServerConfig.SelectCountDown * 1000, ReachTimeLimit);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}
