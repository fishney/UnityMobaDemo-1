/****************************************************
    文件：LobbyWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class LobbyWindow : WindowBase
{
    #region define

    public Text txtName;
    public Text txtLevel;
    public Text txtExp;
    public Image fgExp;
    public Text txtCoin;
    public Text txtDiamond;
    public Text txtTicket;

    public Transform transMatchRoot;
    public Text txtPreTime;
    public Text txtCountTime;
    
    private PlayerData pd;

    #endregion
    
    protected override void InitWindow()
    {
        base.InitWindow();
        SetActive(transMatchRoot,false);
        
        pd = GameRoot.Instance().PlayerData;
        var maxExp = GameRoot.Instance().GetMaxExp(pd.level);
        SetText(txtName,pd.name);
        SetText(txtLevel,"Lv." + pd.level);
        SetText(txtExp,pd.exp + " / " + maxExp);
        SetText(txtCoin,pd.coin);
        SetText(txtDiamond,pd.diamond);
        SetText(txtTicket,pd.ticket);
        fgExp.fillAmount = 1.0f * pd.exp / maxExp;
        SetText(txtPreTime,"00:00");
        SetText(txtCountTime,"00:00");
        
    }




    private int timeCount = 0;
    private float deltaSum = 0;// 用于计算1秒,满1秒就变化一下timeCount
    private bool isMatching = false;

    private void Update()
    {
        if (isMatching)
        {
            deltaSum += Time.deltaTime;
            if (deltaSum >= 1)
            {
                deltaSum -= 1;
                timeCount += 1;
            }
            var countTimeStr = GetCountTimeStr(timeCount);
            SetText(txtCountTime,countTimeStr);
        }
    }

    public void ShowMatchInfo(bool isActive,int preTime = 0)
    {
        if (isActive)
        {
            SetActive(transMatchRoot);
            isMatching = true;
            var preTimeStr = GetCountTimeStr(preTime);
            SetText(txtPreTime,"预计匹配时间: " + preTimeStr);
        }
        else
        {
            SetActive(transMatchRoot,false);
            isMatching = false;
            timeCount = 0;
            deltaSum = 0;
        }
    }

    private string GetCountTimeStr(int timeInt)
    {
        int min = timeInt / 60;
        int sec = timeInt % 60;
        // 进行一下补全,9补成09
        string minStr = min < 10 ? "0" + min + ":" : min + ":";
        string secStr = sec < 10 ? "0" + sec : sec.ToString();
        
        return minStr + secStr;
    }




    #region Events

    public void ClickMatchBtn()
    {
        audioSvc.PlayUIAudio("matchBtnClick");
        // 发送网络请求
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.ReqMatch,
            reqMatch = new ReqMatch()
            {
                pvpEnum = PvpEnum._1V1
            }
        };
        netSvc.SendMsg(msg, result =>
        {
            if (result == false) 
            {
                netSvc.InitSvc();
            }
        });
    }
    
    public void ClickRankBtn()
    {
        audioSvc.PlayUIAudio("matchBtnClick");
        // 发送网络请求
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.ReqMatch,
            reqMatch = new ReqMatch()
            {
                pvpEnum = PvpEnum._2V2
            }
        };
        netSvc.SendMsg(msg, result =>
        {
            if (result == false) 
            {
                netSvc.InitSvc();
            }
        });
    }
    
    public void ClickSettings()
    {
        audioSvc.PlayUIAudio("matchBtnClick");
        // 发送网络请求
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.ReqMatch,
            reqMatch = new ReqMatch()
            {
                pvpEnum = PvpEnum._5V5
            }
        };
        netSvc.SendMsg(msg, result =>
        {
            if (result == false) 
            {
                netSvc.InitSvc();
            }
        });
    }

    #endregion
    
    

   
}
