using System;
using System.Collections.Generic;
using PENet;

/// <summary>
/// 网络通信数据协议
/// </summary>
namespace HOKProtocol
{
	public class GameMsg : KCPMsg
	{
        public CMD cmd;
        public ErrorCode error;
		public ReqLogin reqLogin;
		public RspLogin rspLogin;
	}

    #region 登陆相关

    /// <summary>
    /// 登录请求
    /// </summary>
    [Serializable]
    public class ReqLogin
    {
        public string acct { get; set; }
        public string pass { get; set; }
    }

    /// <summary>
    /// 登录回应
    /// </summary>
    [Serializable]
    public class RspLogin
    {
        public PlayerData playerData;
    }

    /// <summary>
    /// 重命名请求
    /// </summary>
    [Serializable]
    public class ReqRename
    {
        public string name { get; set; }
    }

    /// <summary>
    /// 重命名回应
    /// </summary>
    [Serializable]
    public class RspRename
    {
        public string name { get; set; }
    }
    #endregion


    /// <summary>
	/// 用户信息
	/// </summary>
	[Serializable]
    public class PlayerData
    {
        public uint id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int coin { get; set; }
        public int diamond { get; set; }
        public int ticket { get; set; }
        public List<HeroSelectData> heroSelectData;
    }

    [Serializable]
    public class HeroSelectData
    {
        public int heroID { get; set; }
       
        //已拥有
        //本周限免
    }


    /// <summary>
    /// Command协议常数
    /// </summary>
    public enum ErrorCode
    {
        None = 0,

        /// <summary>
        /// 服务端数据异常
        /// </summary>
        ServerDataError,
        ClientDataError,

        /// <summary>
        /// 更新数据库出错
        /// </summary>
        UpdateDBError,

        // 登录相关

        /// <summary>
        /// 账号已登陆
        /// </summary>
        AccountIsOnline,

        /// <summary>
        /// 密码错误
        /// </summary>
        WrongPass,

        /// <summary>
        /// 名字已存在
        /// </summary>
        NameExisted,

        LackLevel,
        LackCoin,
        LackCrystal,
        LackDiamond,
        LackPower,

        TaskError,

    }

    /// <summary>
    /// Command协议常数
    /// </summary>
    public enum CMD
    {
        None = 0,
        // 登录相关
        ReqLogin = 101,
        RspLogin = 102,






    }

}
