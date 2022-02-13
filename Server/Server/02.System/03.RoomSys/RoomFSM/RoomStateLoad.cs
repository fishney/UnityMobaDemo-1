using System;
using System.Collections.Generic;
using System.Text;
using proto.HOKProtocol;

namespace Server
{
    /// <summary>
    /// 战斗加载
    /// </summary>
    public class RoomStateLoad : RoomStateBase
    {
        private int[] percentArr;
        private bool[] loadedArr;

        /// <summary>
        /// 进度数据前几次先略过。
        /// </summary>
        private int notifyCount;

        public RoomStateLoad(PVPRoom r) : base(r)
        {
        }

        public override void Enter()
        {
            int len = room.sessionArr.Count;
            percentArr = new int[len];
            loadedArr = new bool[len];

            GameMsg msg = new GameMsg
            {
                cmd = CMD.NotifyLoadRes,
                notifyLoadRes = new NotifyLoadRes
                {
                    mapId = 101, // TODO 默认地图
                    heroList = new List<BattleHeroData>(),
                }
            };

            // 全玩家通用信息
            for (int i = 0;i < room.SelectArr.Length;i++)
            {
                SelectData sd = room.SelectArr[i];
                msg.notifyLoadRes.heroList.Add(new BattleHeroData
                {
                    heroId = sd.selectId,
                    userName = GetUserName(i),
                });
            }

            // 玩家个体单独信息
            for (int i = 0 ; i < len ; i++)
            {
                msg.notifyLoadRes.posIndex = i;
                room.sessionArr[i].SendMsg(msg);
            }
        }

        private string GetUserName(int posIndex)
        {
            var playerData = CacheSvc.Instance().GetPlayerData(room.sessionArr[posIndex]);
            if (playerData != null)
            {
                return playerData.name;
            }

            return "NO_NAME";
        }


        public void UpdateLoadState(int posIndex, int percent)
        {
            percentArr[posIndex] = percent;
            //++notifyCount;
            //if (notifyCount > percentArr.Length)
            //{
                GameMsg msg = new GameMsg
                {
                    cmd = CMD.NotifyLoadPrg,
                    notifyLoadPrg = new NotifyLoadPrg
                    {
                        percentList = new List<int>(),
                    }
                };

                msg.notifyLoadPrg.percentList.AddRange(percentArr);

                room.PublishMsg(msg);
            //}

           
        }

        public void UpdateLoadDone(int posIndex)
        {
            loadedArr[posIndex] = true;

            foreach (var loaded in loadedArr)
            {
                if (!loaded) return;
            }

            // 全部加载完成
            GameMsg msg = new GameMsg
            {
                cmd = CMD.RspBattleStart,
                rspBattleStart = new RspBattleStart
                {

                }
            };

            room.PublishMsg(msg);

            this.ColorLog(PEUtils.LogColor.Green,$"RoomId:{room.roomId}: 所有玩家加载完成,进入游戏");
            room.ChangeRoomState(RoomStateEnum.Fight);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            percentArr = null;
            loadedArr = null;
        }

    }
}
