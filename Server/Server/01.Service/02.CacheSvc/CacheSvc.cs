using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
	public class CacheSvc : ServiceBase<CacheSvc>
	{

        /// <summary>
        /// 账号在线字典
        /// </summary>
        private Dictionary<string, ServerSession> onlineAcctDic;

        /// <summary>
        /// 在线账号信息字典
        /// </summary>
        private Dictionary<ServerSession, PlayerData> onlineSessionDic;

        /// <summary>
        /// 根据账号密码返回 PlayerData 账号数据，密码错误返回null，账号不存在则创建默认账号
        /// </summary>
        public PlayerData GetPlayerData(string acct, string pass)
        {
            return DBMgr.Instance().QueryPlayerData(acct, pass);
        }

        /// <summary>
        /// 根据session返回 PlayerData 数据
        /// </summary>
        public PlayerData GetPlayerData(ServerSession session)
        {
            if (onlineSessionDic.TryGetValue(session,out var playerData))
            {
                return playerData;
            }
            else
            {
                return null;
            }
        }

        #region base
        private CacheSvc() { }

        public override void Init()
        {
            base.Init();

            onlineAcctDic = new Dictionary<string, ServerSession>();
            onlineSessionDic = new Dictionary<ServerSession, PlayerData>();
        }

        public override void Update()
        {
            base.Update();
        }
		#endregion

        public bool IsAccOnline(string acct)
        {
            return onlineAcctDic.ContainsKey(acct);
        }

        /// <summary>
        /// 帐号上线，缓存数据
        /// </summary>
        public void AcctOnline(string acct, ServerSession session, PlayerData playerData)
        {
            onlineAcctDic.Add(acct, session);
            onlineSessionDic.Add(session, playerData);
        }

        /// <summary>
        /// 帐号登出时，清空本地缓存中该账号记录 
        /// </summary>
        internal void AcctOffline(ServerSession session)
        {
            foreach (var item in onlineAcctDic)
            {
                if (item.Value == session)
                {
                    onlineAcctDic.Remove(item.Key);
                    break;
                }
            }

            string succ = onlineSessionDic.Remove(session) ? "successed" : "failed";
            this.Log("Offline Result SessionId:" + session.sessionId + " " + succ);
        }

    }
}
