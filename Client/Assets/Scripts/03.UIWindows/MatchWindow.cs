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
        // 计算左侧右侧需要哪几个显示
        int count = confirmArr.Length / 2;

        // 左侧
        for (int i = 0; i < 5; i++)
        {
            var player = leftPlayerRoot.GetChild(i).transform;
            if (i < count)
            {
                SetActive(player);
                var iconPath = Constants.IconPath + confirmArr[i].iconIndex;
                var iconFramePath = Constants.IconFramePath + (confirmArr[i].confirmDone ? "sure" : "normal");

                var imgIcon = GetImage(player);
                SetSprite(imgIcon, iconPath);

                var imgFrame = GetImage(player, "img_state");
                SetSprite(imgFrame, iconFramePath);

                imgFrame.SetNativeSize();
            }
            else
            {
                SetActive(player, false);
            }
        }

        // 右侧
        for (int i = 0; i < 5; i++)
        {
            var player = rightPlayerRoot.GetChild(i).transform;
            if (i < count)
            {
                SetActive(player);
                var iconPath = Constants.IconPath + confirmArr[i + count].iconIndex;
                var iconFramePath = Constants.IconFramePath + (confirmArr[i + count].confirmDone ? "sure" : "normal");

                var imgIcon = GetImage(player);
                SetSprite(imgIcon, iconPath);

                var imgFrame = GetImage(player, "img_state");
                SetSprite(imgFrame, iconFramePath);

                imgFrame.SetNativeSize();
            }
            else
            {
                SetActive(player, false);
            }
        }

        int cofirmCount = 0;
        for (int i = 0; i < confirmArr.Length; i++)
        {
            if (confirmArr[i].confirmDone)
            {
                cofirmCount++;
            }
        }

        txtConfirm.text = cofirmCount + "/" + confirmArr.Length + "就绪";
    }

    public void ClickConfirmBtn()
    {
        audioSvc.PlayUIAudio("matchSureClick");

        GameMsg msg = new GameMsg()
        {
            cmd = CMD.SendConfirm,
            sendConfirm = new SendConfirm()
            {
                roomId = GameRoot.ActiveRoomId
            }
        };
        
        netSvc.SendMsg(msg);
        btnConfirm.interactable = false;
    }
    
    
}
