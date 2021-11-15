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
	        checkTaskId = TimerSvc.Instance().AddTask(ServerConfig.SelectCountDown * 1000 + 2000, ReachTimeLimit);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            selectArr = null;
            checkTaskId = 0;
            isAllSelected = false;
        }

        public void UpdateHeroSelect(int posIndex,int heroId)
        {
            selectArr[posIndex].selectId = heroId;
            selectArr[posIndex].selectDone = true;
            CheckSelectState();

            if (isAllSelected)
            {
                // 取消掉超时的回调动作
                if (TimerSvc.Instance().DeleteTask(checkTaskId))
                {
                    this.ColorLog(PEUtils.LogColor.Green, $"RoomID:{room.roomId} 所有玩家选择完成，进入游戏加载。");
                }
                else
                {
                    this.Warn($"RoomID:{room.roomId} 取消定时回调任务ReachTimeLimit失败。");
                }

                room.SelectArr = selectArr;
                room.ChangeRoomState(RoomStateEnum.Load);
            }
            else
            {
                //GameMsg msg = new GameMsg
                //{
                //    cmd = CMD.NotifyConfirm,
                //    notifyConfirm = new NotifyConfirm
                //    {
                //        roomId = room.roomId,
                //        dismiss = false,
                //        confirmArr = confirmArr,
                //    },
                //};

                //room.PublishMsg(msg);

            }

        }

        /// <summary>
        /// 客户端那虽然也有计时会发,但是如果用户断线了就发不了了,此时服务端这里进行处理.
        /// </summary>
        private void ReachTimeLimit(int tid)
        {
            if (isAllSelected)
            {
                return;
            }
            else
            {
                this.Warn($"RoomID:{room.roomId} 玩家超时未选择确认,指定默认英雄");
                for (int i = 0 ; i < selectArr.Length ; i++)
                {
                    if (selectArr[i].selectDone == false)
                    {
                        selectArr[i].selectId = GetDefaultHeroId(i);
                        selectArr[i].selectDone = true;
                    }
                }

                room.SelectArr = selectArr;
                room.ChangeRoomState(RoomStateEnum.Load);
            }
        }

        private int GetDefaultHeroId(int posIndex)
        {
            var playerData = CacheSvc.Instance().GetPlayerData(room.sessionArr[posIndex]);
            if (playerData != null)
            {
                return playerData.heroSelectData[0].heroID;
            }

            return 101;
        }

        /// <summary>
        /// 更新flag isAllConfirmed
        /// </summary>
        private void CheckSelectState()
        {
            for (int i = 0; i < selectArr.Length; i++)
            {
                if (selectArr[i].selectDone == false)
                {
                    return;
                }
            }

            isAllSelected = true;
        }

    }
}
