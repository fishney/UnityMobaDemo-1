using System;
using System.Collections.Generic;
using PENet;

/// <summary>
/// 网络通信数据协议
/// </summary>
namespace HOKProtocol
{
    [Serializable]
    public class GameMsg : KCPMsg
	{
        public CMD cmd;
        public ErrorCode err;
        public bool isEmpty;

		public ReqLogin reqLogin;
		public RspLogin rspLogin;

		public ReqMatch reqMatch;
		public RspMatch rspMatch;
        public NotifyConfirm notifyConfirm;
        public SendConfirm sendConfirm;
        public NotifySelect notifySelect;
        public SendSelect sendSelect;

        public NotifyLoadRes notifyLoadRes;
        public SendLoadPrg sendLoadPrg;
        public NotifyLoadPrg notifyLoadPrg;

        public ReqBattleStart reqBattleStart;
        public RspBattleStart rspBattleStart;
        public ReqBattleEnd reqBattleEnd;
        public RspBattleEnd rspBattleEnd;

        public SendChat sendChat;
        public NotifyChat notifyChat;

        public SendOpKey sendOpKey;
        public NotifyOpKey notifyOpKey;

        public ReqPing reqPing;
        public RspPing rspPing;
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


    #region 匹配确认相关

    /// <summary>
    /// 匹配请求
    /// </summary>
    [Serializable]
    public class ReqMatch
    {
        /// <summary>
        /// 匹配类型
        /// </summary>
        public PvpEnum pvpEnum;
    }

    /// <summary>
    /// 匹配回应
    /// </summary>
    [Serializable]
    public class RspMatch
    {
        /// <summary>
        /// 预计等待时间
        /// </summary>
        public int preTime;
    }

    /// <summary>
    /// 从服务端发出的房间确认状况信息
    /// </summary>
    [Serializable]
    public class NotifyConfirm
    {
        public int roomId;

        /// <summary>
        /// 是否本次匹配被解散(某人取消)
        /// </summary>
        public bool dismiss;


        /// <summary>
        /// 同房间匹配者们的信息
        /// </summary>
        public ConfirmData[] confirmArr;
    }

    /// <summary>
    /// 确认界面用的人物信息
    /// </summary>
    [Serializable]
    public class ConfirmData
    {
        /// <summary>
        /// 该玩家头像数据地址(模拟)
        /// </summary>
        public int iconIndex;

        /// <summary>
        /// 该玩家是否已经确认了
        /// </summary>
        public bool confirmDone;
    }

    /// <summary>
    /// 从客户端发来确认对局的消息
    /// </summary>
    [Serializable]
    public class SendConfirm
    {
        public int roomId;
    }




    /// <summary>
    /// 选择英雄请求
    /// </summary>
    [Serializable]
    public class NotifySelect
    {
	    public SelectData selectData;
    }

    /// <summary>
    /// 选择英雄数据
    /// </summary>
    [Serializable]
    public class SelectData
    {
	    /// <summary>
	    /// 选择英雄ID
	    /// </summary>
	    public int selectId;

	    /// <summary>
	    /// 选择状态
	    /// </summary>
	    public bool selectDone;
    }

    /// <summary>
    /// 从客户端发来确认对局的英雄信息
    /// </summary>
    [Serializable]
    public class SendSelect
    {
	    public int roomId;
	    public int heroId;
    }

    /// <summary>
    /// 从客户端发来确认对局的英雄信息
    /// </summary>
    [Serializable]
    public class NotifyLoadRes
    {
        public int mapId;
        public List<BattleHeroData> heroList;

        ///玩家在房间玩家List里的相对位置
        public int posIndex;
    }

    /// <summary>
    /// 推送进度
    /// </summary>
    [Serializable]
    public class SendLoadPrg
    {
        public int roomId;
        public int percent;
    }

    /// <summary>
    /// 推送进度
    /// </summary>
    [Serializable]
    public class NotifyLoadPrg
    {
        public List<int> percentList;
    }

    #endregion

    #region 核心战斗

    [Serializable]
    public class ReqBattleStart
    {
        public int roomId;
    }

    [Serializable]
    public class RspBattleStart
    {
        
    }

    [Serializable]
    public class ReqBattleEnd
    {
	    public int roomId;
    }

    [Serializable]
    public class RspBattleEnd
    {
        // 结算数据
    }

    [Serializable]
    public class SendOpKey
    {
        public int roomId;
        public OpKey opKey;
    }

    [Serializable]
    public class NotifyOpKey
    {
        public int frameId;
        public List<OpKey> keyList;
    }

    [Serializable]
    public class SendChat
    {
	    public int roomId;
	    public string chatMsg;
    }

    [Serializable]
    public class NotifyChat
    {
	    public string chatMsg;
    }

    [Serializable]
    public class ReqPing
    {
	    public uint pingId;
	    public ulong sendTime;
	    public ulong backTime;
    }

    [Serializable]
    public class RspPing
    {
	    public uint pingId;
    }
    
    #endregion

    #region 数据类型

    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int coin { get; set; }
        public int diamond { get; set; }
        public int ticket { get; set; }
        public List<HeroSelectData> heroSelectData;
    }

    /// <summary>
    /// 英雄数据
    /// </summary>
    [Serializable]
    public class HeroSelectData
    {
        public int heroID { get; set; }

        //已拥有
        //本周限免
    }

    /// <summary>
    /// 战场英雄数据
    /// </summary>
    [Serializable]
    public class BattleHeroData
    {
        public string userName { get; set; }
        public int heroId { get; set; }

        //级别,皮肤id,边框
    }

    #endregion



    /// <summary>
    /// 匹配类型
    /// </summary>
    public enum PvpEnum
    {
        None = 0,
        _1V1 = 1,
        _2V2 = 2,
        _5V5 = 3,

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

        // 匹配
        ReqMatch = 103,
        RspMatch = 104,

        // 确认
        NotifyConfirm = 105,
        SendConfirm = 106,

        // 选择英雄
        NotifySelect = 107,
        SendSelect = 108,

        // 加载
        NotifyLoadRes = 109,
        SendLoadPrg = 110,
        NotifyLoadPrg = 111,

        // 战斗
        ReqBattleStart = 112,
        RspBattleStart = 113,
        ReqBattleEnd = 114,
        RspBattleEnd = 115,

        //PING
        ReqPing = 1,
        RspPing = 2,

        // 聊天
        SendChat = 201,
        NotifyChat = 202,

        // 操作码
        SendOpKey = 1000,
        NotifyOpKey = 1001,

    }

   

}
