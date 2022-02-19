using System;
using System.Collections.Generic;
using System.Text;
using proto.HOKProtocol;
using System.Linq;

namespace Server
{
    internal class LobbySys : SystemBase<LobbySys>
    {
        private LobbySys(){}

        public override void Init()
        {
            base.Init();

        }

        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 处理 背包请求消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReqBagItem(MsgPack msgPack)
        {
            // 当前帐号是否已上线，若未上线账号是否存在，若账号密码是否正确，否则皆返回失败
            ReqBagItem data = msgPack.msg.reqBagItem;

            GameMsg msg = new GameMsg
            {
                cmd = CMD.RspBagItem,
            };

            // 更新本地数据，并计算出返回效果
            var localPlayerData = cacheSvc.GetPlayerData(msgPack.session);
            if (localPlayerData == null)
            {
                this.Log("SessionId {0} 账号下线导致物品使用失败",msgPack.session.GetSessionID());
                return;
            }

            // TODO 可能出错
            var item = localPlayerData.bagData.FirstOrDefault(o => o.itemId == data.itemId && o.itemNum >= data.itemNum);

            if (item == null)
            {
                msg.err = ErrorCode.BagItemError;
                msgPack.session.SendMsg(msg);
                return;
            }

            // TODO 读表并计算出返回效果
            item.itemNum -= data.itemNum;
            localPlayerData.coin += 300;
            localPlayerData.diamond += 300;

            var addedExp = 300;
            if (addedExp > 0)
            {
                localPlayerData.exp += addedExp;
                var needExp = GetMaxExp(localPlayerData.level);
                while (localPlayerData.exp >= needExp)
                {
                    localPlayerData.level++;
                    localPlayerData.exp -= needExp;
                    needExp = GetMaxExp(localPlayerData.level);
                }
            }
            // 更新数据库数据
            cacheSvc.UpdatePlayerData(localPlayerData);

            // 返回请求
            msg.rspBagItem = new RspBagItem
            {
                updatedPlayerData = localPlayerData,
                usedItem = item.itemId,
            };

            msgPack.session.SendMsg(msg);
        }

        public int GetMaxExp(int lv)
        {
            return (lv + 1) * 1000;
        }
    }
}
