/****************************************************
    文件：GameRoot.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：2021-11-1
    功能：启动
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using PEUtils;
using UnityEngine;

public class GameRoot : GameRootMonoSingleton<GameRoot>
{
    private void Start()
    {
        LogConfig cfg = new LogConfig()
        {
            enableLog = true,
            logPrefix = "",
            enableTime = true,
            logSeparate = ">",
            enableThreadID = true,// log显示线程id
            enableTrace = false,
            enableSave = true,
            enableCover = true,// 和上一次做对比进行覆盖
            saveName = "HOKClient_PELog.txt",
            loggerType = LoggerType.Unity,
        };
        PELog.InitSettings(cfg);
        PELog.ColorLog(LogColor.Green,"GameRoot Init...");
        
        DontDestroyOnLoad(this);
        ClearUIRoot();
        Init();
    }

    private void Update()
    {
        if (tempTimerList.Count > 0)
        {
            timerList.AddRange(tempTimerList);
            tempTimerList.Clear();
        }

        for (int i = timerList.Count - 1; i >= 0; --i)
        {
            MonoTimer timer = timerList[i];
            if (timer.IsActive)
            {
                timer.TickTimer(Time.deltaTime * 1000);
            }
            else
            {
                timerList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 初始化所有窗口，隐藏他们，除了显示Tips的DynamicWindow
    /// </summary>
    private void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        
        GameRootResources.Instance().tipsWindow.SetWindowState(true);
    }
    

    private NetSvc netSvc;
    private AudioSvc audioSvc;
    private ResSvc resSvc;

    private void Init()
    {
        tempTimerList = new List<MonoTimer>();
        timerList = new List<MonoTimer>();

        netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();
        audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();
        resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();

        LoginSys loginSys = GetComponent<LoginSys>();
        loginSys.InitSys();
        LobbySys lobbySys = GetComponent<LobbySys>();
        lobbySys.InitSys();
        BattleSys battleSys = GetComponent<BattleSys>();
        battleSys.InitSys();
        GMSystem gmSys = GetComponent<GMSystem>();
        gmSys.InitSys();
        
        
        //进入登陆场景并加载相应UI
        loginSys.EnterLogin();
    }
    
    /// <summary>
    /// 用户数据
    /// </summary>
    public PlayerData PlayerData
    {
        get;
        private set;
    }
    
    public void SetPlayerData(PlayerData pd)
    {
        PlayerData = pd;
    }



    #region 客户端全局信息

    /// <summary>
    /// 游戏中房间ID
    /// </summary>
    public static int ActiveRoomId;
    
    /// <summary>
    /// 地图Id
    /// </summary>
    public static int MapId;
    
    /// <summary>
    /// 玩家在房间玩家List里的相对位置
    /// </summary>
    public static int SelfPosIndex;

    /// <summary>
    /// 房间内所有玩家信息
    /// </summary>
    public static List<BattleHeroData> battleHeroList;

    #endregion
    
    public int GetMaxExp(int lv)
    {
        return (lv+1) * 1000;
    }

    private List<MonoTimer> tempTimerList;
    private List<MonoTimer> timerList;
    public void AddMonoTimer(MonoTimer timer)
    {
        tempTimerList.Add(timer);
    }
    
}
