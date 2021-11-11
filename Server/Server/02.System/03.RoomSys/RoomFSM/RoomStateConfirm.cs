using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
    /// <summary>
    /// 等待确认
    /// </summary>
    public class RoomStateConfirm : RoomStateBase
    {
        /// <summary>
        /// 房间内玩家信息,是否确认
        /// </summary>
        private ConfirmData[] confirmArr = null;
        private int checkTaskId;

        public RoomStateConfirm(PVPRoom r) : base(r)
        {
            var num = r.sessionArr.Length;
            confirmArr = new ConfirmData[num];
        }

        public override void Enter()
        {
            
            for (int i = 0; i < confirmArr.Length; i++)
            {
                confirmArr[i] = new ConfirmData
                {
                    iconIndex = i,
                    confirmDone = false,
                };
            }

            GameMsg msg = new GameMsg
            {
                cmd = CMD.NotifyConfirm,
                notifyConfirm = new NotifyConfirm
                {
                    roomId = room.roomId,
                    dismiss = false,
                    confirmArr = confirmArr,
                },
            };

            room.PublishMsg(msg);
            checkTaskId = TimerSvc.Instance().AddTask(ServerConfig.ConfirmCountDown*1000, ReachTimeLimit);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }

        private void ReachTimeLimit(int tid)
        {

        }


    }
}
