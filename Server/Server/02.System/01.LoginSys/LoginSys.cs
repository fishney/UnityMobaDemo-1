using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
	public class LoginSys : SystemBase<LoginSys>
	{
		private LoginSys(){}

		public override void Init()
		{
			base.Init();
			
		}

		public override void Update()
		{
			base.Update();
		}

        /// <summary>
        /// 处理 登录请求消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReqLogin(MsgPack msgPack)
        {
            // 当前帐号是否已上线，若未上线账号是否存在，若账号密码是否正确，否则皆返回失败
            ReqLogin data = msgPack.msg.reqLogin;

            GameMsg msg = new GameMsg
            {
                cmd = CMD.RspLogin,
            };

            // 当前帐号是否已上线
            if (cacheSvc.IsAccOnline(data.acct))
            {
                msg.err = ErrorCode.AccountIsOnline;
            }
            else
            {
                PlayerData pd = cacheSvc.GetPlayerData(data.acct, data.pass);

                if (pd == null)
                {
                    // 为空，密码错误
                    msg.err = ErrorCode.WrongPass;
                }
                else
                {
                    // 计算离线体力
                    //int power = pd.power;
                    //long now = timerSvc.GetNowTime();
                    //long milliseconds = now - pd.time;
                    //int addPower = (int)(milliseconds / (1000 * 60 * PECommon.PowerAddSpace)) * PECommon.PowerAddCount;
                    //if (addPower > 0)
                    //{
                    //    int powerMax = PECommon.GetPowerLimit(pd.level);
                    //    // 最高只能回复到上限值
                    //    pd.power = pd.power + addPower > powerMax ? powerMax : pd.power + addPower;
                    //    cacheSvc.UpdatePlayerData(pd);
                    //}

                    // 登录认证成功
                    msg.rspLogin = new RspLogin
                    {
                        playerData = pd
                    };

                    cacheSvc.AcctOnline(data.acct, msgPack.session, pd);
                }
            }

            msgPack.session.SendMsg(msg);
        }

        /// <summary>
        /// 帐号登出时，清空本地缓存中该账号记录 
        /// </summary>
        public void ClearOfflineData(ServerSession session)
        {
            cacheSvc.AcctOffline(session);
        }
    }
}
