/****************************************************
    文件：MatchWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class MatchWindow : WindowBase
{
    #region define

    public Text txtTime;
    public Text txtConfirm;

    public Transform leftPlayerRoot;
    public Transform rightPlayerRoot;
    public Button btnConfirm;

    private int timeCount;
    #endregion
    
    protected override void InitWindow()
    {
        base.InitWindow();

        timeCount = ServerConfig.ConfirmCountDown;
        btnConfirm.interactable = true;
        audioSvc.PlayUIAudio("matchReminder");
    }

    private float deltaCount;
    private void Update()
    {
        deltaCount += Time.deltaTime;
        if (deltaCount >= 1)
        {
            deltaCount -= 1;
            timeCount -= 1;
            if (timeCount < 0)
            {
                timeCount = 0;
            }
            
            SetText(txtTime,timeCount);
        }
    }
    
    public void RefreshUI(ConfirmData[] confirmArr)
    {
        int count = confirmArr.Length / 2;
        
    }
}
