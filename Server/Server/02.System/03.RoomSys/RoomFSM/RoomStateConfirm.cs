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
        private bool isAllConfirmed;

        public RoomStateConfirm(PVPRoom r) : base(r)
        {
            var num = r.sessionArr.Count;
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
	        if (isAllConfirmed)
	        {
                return;
			}
	        else
	        {
		        this.ColorLog(PEUtils.LogColor.Yellow, $"RoomID:{room.roomId} 确认超时，重新匹配。");
		        
		        GameMsg msg = new GameMsg
		        {
			        cmd = CMD.NotifyConfirm,
			        notifyConfirm = new NotifyConfirm
			        {
				        dismiss = true,
			        },
		        };

		        room.PublishMsg(msg);
            }
        }

        public void UpdateConfirmState(int posIndex)
        {
            confirmArr[posIndex].confirmDone = true;
            CheckConfirmState();

            if (isAllConfirmed)
            {
                // 取消掉超时的回调动作
	            if (TimerSvc.Instance().DeleteTask(checkTaskId))
	            {
		            this.ColorLog(PEUtils.LogColor.Green,$"RoomID:{room.roomId} 所有玩家确认，进入英雄选择。");
				}
				else
	            {
		            this.Warn($"RoomID:{room.roomId} 取消定时回调任务ReachTimeLimit失败。");
	            }

	            room.ChangeRoomState(RoomStateEnum.Select);
            }
            else
            {
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

            }

        }

        /// <summary>
        /// 更新flag isAllConfirmed
        /// </summary>
        private void CheckConfirmState()
        {
	        for (int i = 0;i < confirmArr.Length; i++)
	        {
		        if (confirmArr[i].confirmDone == false)
		        {
                    return;
		        }
	        }

            isAllConfirmed = true;
        }
    }
}
