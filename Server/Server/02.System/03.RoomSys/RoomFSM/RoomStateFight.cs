using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
    /// <summary>
    /// 战斗中
    /// </summary>
    public class RoomStateFight : RoomStateBase
    {
        private int frameId = 0;
        private List<OpKey> opKeyList = new List<OpKey>();
        private int checkTaskId;

        /// <summary>
        /// 点结束的用户列表
        /// </summary>
        private bool[] endArr;

        public RoomStateFight(PVPRoom r) : base(r)
        {
	        endArr = new bool[room.sessionArr.Count];
        }

        public override void Enter()
        {
            opKeyList.Clear();
            checkTaskId = TimerSvc.Instance().AddTask(Configs.ServerLogicFrameIntervelMs, SyncLogicFrame,null,0);
        }

        void SyncLogicFrame(int tid)
        {
            ++frameId;
            GameMsg msg = new GameMsg
            {
                cmd = CMD.NotifyOpKey,
                isEmpty = true,
                notifyOpKey = new NotifyOpKey
                {
                    frameId = frameId,
                    keyList = new List<OpKey>(),
                }
            };

            int count = opKeyList.Count;
            if (count > 0)
            {
                msg.isEmpty = false;
                msg.notifyOpKey.keyList.AddRange(opKeyList);
            }
            opKeyList.Clear();
            room.PublishMsg(msg);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            checkTaskId = 0;
            opKeyList.Clear();
            endArr = null;
        }

        public void UpdateOpKey(OpKey key)
        {
            opKeyList.Add(key);
        }

        public void UpdateEndState(int posIndex)
        {
            endArr[posIndex] = true;

	        if (TimerSvc.Instance().DeleteTask(checkTaskId))
	        {
		        this.ColorLog(PEUtils.LogColor.Green,"Delete Sync Task Success.");
			}
	        else
	        {
		        this.Warn("Delete Sync Task Failed.");
	        }

	        room.ChangeRoomState(RoomStateEnum.End);
        }
    }
}
