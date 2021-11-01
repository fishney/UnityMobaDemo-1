/****************************************************
    文件：GameRoot.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：2021-11-1
    功能：启动
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using PEUtils;
using UnityEngine;

public class GameRoot : MonoBehaviour
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

        // ClearUIRoot();
        Init();
    }


    private void Init()
    {
        
    }
}
