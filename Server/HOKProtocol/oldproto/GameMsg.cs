using System;
using System.Collections.Generic;
using CodingK_Session;
using ProtoBuf;

/// <summary>
/// 网络通信数据协议
/// </summary>
namespace HOKProtocol
{
    [Serializable]
    [ProtoContract]
    public class GameMsg : CodingK_Msg
	{
        [ProtoMember(1)]
        public CMD cmd;
        [ProtoMember(2)]
        public ErrorCode err;
        [ProtoMember(3)]
        public bool isEmpty;

        [ProtoMember(4)]
        public ReqLogin reqLogin;
        [ProtoMember(5)]
        public RspLogin rspLogin;

        [ProtoMember(6)]
		public ReqMatch reqMatch;
        [ProtoMember(7)]
		public RspMatch rspMatch;
        [ProtoMember(8)]
        public NotifyConfirm notifyConfirm;
        [ProtoMember(9)]
        public SendConfirm sendConfirm;
        [ProtoMember(10)]
        public NotifySelect notifySelect;
        [ProtoMember(11)]
        public SendSelect sendSelect;

        [ProtoMember(12)]
        public NotifyLoadRes notifyLoadRes;
        [ProtoMember(13)]
        public SendLoadPrg sendLoadPrg;
        [ProtoMember(14)]
        public NotifyLoadPrg notifyLoadPrg;

        [ProtoMember(15)]
        public ReqBattleStart reqBattleStart;
        [ProtoMember(16)]
        public RspBattleStart rspBattleStart;
        [ProtoMember(17)]
        public ReqBattleEnd reqBattleEnd;
        [ProtoMember(18)]
        public RspBattleEnd rspBattleEnd;

        [ProtoMember(19)]
        public SendChat sendChat;
        [ProtoMember(20)]
        public NotifyChat notifyChat;

        [ProtoMember(21)]
        public SendOpKey sendOpKey;
        [ProtoMember(22)]
        public NotifyOpKey notifyOpKey;

        [ProtoMember(23)]
        public ReqPing reqPing;
        [ProtoMember(24)]
        public RspPing rspPing;
    }

    #region 登陆相关

    /// <summary>
    /// 登录请求
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class ReqLogin
    {
        [ProtoMember(1)]
        public string acct { get; set; }
        [ProtoMember(2)]
        public string pass { get; set; }
    }

    /// <summary>
    /// 登录回应
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class RspLogin
    {
        [ProtoMember(1)]
        public PlayerData playerData;
    }

    /// <summary>
    /// 重命名请求
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class ReqRename
    {
        [ProtoMember(1)]
        public string name { get; set; }
    }

    /// <summary>
    /// 重命名回应
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class RspRename
    {
        [ProtoMember(1)]
        public string name { get; set; }
    }
    #endregion


    #region 匹配确认相关

    /// <summary>
    /// 匹配请求
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class ReqMatch
    {
        /// <summary>
        /// 匹配类型
        /// </summary>
        [ProtoMember(1)] 
        public PvpEnum pvpEnum;
    }

    /// <summary>
    /// 匹配回应
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class RspMatch
    {
        /// <summary>
        /// 预计等待时间
        /// </summary>
        [ProtoMember(1)] 
        public int preTime;
    }

    /// <summary>
    /// 从服务端发出的房间确认状况信息
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class NotifyConfirm
    {
        [ProtoMember(1)]
        public int roomId;

        /// <summary>
        /// 是否本次匹配被解散(某人取消)
        /// </summary>
        [ProtoMember(2)] 
        public bool dismiss;


        /// <summary>
        /// 同房间匹配者们的信息
        /// </summary>
        [ProtoMember(3)]
        public ConfirmData[] confirmArr;
    }

    /// <summary>
    /// 确认界面用的人物信息
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class ConfirmData
    {
        /// <summary>
        /// 该玩家头像数据地址(模拟)
        /// </summary>
        [ProtoMember(1)] 
        public int iconIndex;

        /// <summary>
        /// 该玩家是否已经确认了
        /// </summary>
        [ProtoMember(2)] 
        public bool confirmDone;
    }

    /// <summary>
    /// 从客户端发来确认对局的消息
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class SendConfirm
    {
        [ProtoMember(1)]
        public int roomId;
    }

    /// <summary>
    /// 选择英雄请求
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class NotifySelect
    {
        [ProtoMember(1)]
        public SelectData selectData;
    }

    /// <summary>
    /// 选择英雄数据
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class SelectData
    {
        /// <summary>
        /// 选择英雄ID
        /// </summary>
        [ProtoMember(1)] 
        public int selectId;

        /// <summary>
        /// 选择状态
        /// </summary>
        [ProtoMember(2)] 
        public bool selectDone;
    }

    /// <summary>
    /// 从客户端发来确认对局的英雄信息
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class SendSelect
    {
        [ProtoMember(1)]
        public int roomId;
        [ProtoMember(2)]
        public int heroId;
    }

    /// <summary>
    /// 从客户端发来确认对局的英雄信息
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class NotifyLoadRes
    {
        [ProtoMember(1)]
        public int mapId;
        [ProtoMember(2)]
        public List<BattleHeroData> heroList;

        ///玩家在房间玩家List里的相对位置
        [ProtoMember(3)]
        public int posIndex;
    }

    /// <summary>
    /// 推送进度
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class SendLoadPrg
    {
        [ProtoMember(1)]
        public int roomId;
        [ProtoMember(2)]
        public int percent;
    }

    /// <summary>
    /// 推送进度
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class NotifyLoadPrg
    {
        [ProtoMember(1)]
        public List<int> percentList;
    }

    #endregion

    #region 核心战斗

    [Serializable]
    [ProtoContract]
    public class ReqBattleStart
    {
        [ProtoMember(1)]
        public int roomId;
    }

    [Serializable]
    [ProtoContract]
    public class RspBattleStart
    {
        
    }

    [Serializable]
    [ProtoContract]
    public class ReqBattleEnd
    {
        [ProtoMember(1)]
        public int roomId;
    }

    [Serializable]
    [ProtoContract]
    public class RspBattleEnd
    {
        // 结算数据
    }

    [Serializable]
    [ProtoContract]
    public class SendOpKey
    {
        [ProtoMember(1)]
        public int roomId;
        [ProtoMember(2)]
        public OpKey opKey;
    }

    [Serializable]
    [ProtoContract]
    public class NotifyOpKey
    {
        [ProtoMember(1)]
        public int frameId;
        [ProtoMember(2)]
        public List<OpKey> keyList;
    }

    [Serializable]
    [ProtoContract]
    public class SendChat
    {
        [ProtoMember(1)]
        public int roomId;
        [ProtoMember(2)]
        public string chatMsg;
    }

    [Serializable]
    [ProtoContract]
    public class NotifyChat
    {
        [ProtoMember(1)]
        public string chatMsg;
    }

    [Serializable]
    [ProtoContract]
    public class ReqPing
    {
        [ProtoMember(1)]
        public uint pingId;
        [ProtoMember(2)]
        public ulong sendTime;
        [ProtoMember(3)]
        public ulong backTime;
    }

    [Serializable]
    [ProtoContract]
    public class RspPing
    {
        [ProtoMember(1)]
        public uint pingId;
    }
    
    #endregion

    #region 数据类型

    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class PlayerData
    {
        [ProtoMember(1)]
        public int id { get; set; }
        [ProtoMember(2)]
        public string name { get; set; }
        [ProtoMember(3)]
        public int level { get; set; }
        [ProtoMember(4)]
        public int exp { get; set; }
        [ProtoMember(5)]
        public int coin { get; set; }
        [ProtoMember(6)]
        public int diamond { get; set; }
        [ProtoMember(7)]
        public int ticket { get; set; }

        [ProtoMember(8)]
        public List<HeroSelectData> heroSelectData;
    }

    /// <summary>
    /// 英雄数据
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class HeroSelectData
    {
        [ProtoMember(1)]
        public int heroID { get; set; }

        //已拥有
        //本周限免
    }

    /// <summary>
    /// 战场英雄数据
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class BattleHeroData
    {
        [ProtoMember(1)]
        public string userName { get; set; }
        [ProtoMember(2)]
        public int heroId { get; set; }

        //级别,皮肤id,边框
    }

    #endregion



    /// <summary>
    /// 匹配类型
    /// </summary>
    [ProtoContract]
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
    [ProtoContract]
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
    [ProtoContract]
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
