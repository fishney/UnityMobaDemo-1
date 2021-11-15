using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
    /// <summary>
    /// 战斗加载
    /// </summary>
    public class RoomStateLoad : RoomStateBase
    {
        private int[] percentArr;

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
            ++notifyCount;
            if (notifyCount > percentArr.Length)
            {
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
            }

           
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }


    }
}
