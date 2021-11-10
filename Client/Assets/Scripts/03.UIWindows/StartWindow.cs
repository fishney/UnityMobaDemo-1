/****************************************************
    文件：StartWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : WindowBase
{
    #region define

    public Text txtName;
    private PlayerData pd;

    #endregion
    
    protected override void InitWindow()
    {
        base.InitWindow();
        pd = GameRoot.Instance().PlayerData;
        txtName.text = pd.name;
    }
    
    public void ClickAreaBtn()
    {
        // TODO 
        ShowTips("开发中...");
    }

    public void ClickStartBtn()
    {
        audioSvc.PlayUIAudio("com_click1");
        LoginSys.Instance.EnterLobby();
    }
    
    public void ClickExitBtn()
    {
        // TODO
        ShowTips("开发中...");
    }
}
