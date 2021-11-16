/****************************************************
    文件：LoginWindow.cs
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

public class LoginWindow : WindowBase
{
    #region define

    public InputField inputAccount;
    public InputField inputPassword;
    public Toggle togServer;

    #endregion

    protected override void InitWindow()
    {
        base.InitWindow();
        
        // 获取本地存储的账号密码
        if (PlayerPrefs.HasKey("Account") && PlayerPrefs.HasKey("Password"))
        {
            inputAccount.text = PlayerPrefs.GetString("Account");
            inputPassword.text = PlayerPrefs.GetString("Password");
        }
        else
        {
            inputAccount.text = string.Empty;
            inputPassword.text = string.Empty;
        }
    }

    #region Events

    public void ClickLoginBtn()
    {
        audioSvc.PlayUIAudio(Constants.LoginBtn);

        string account = inputAccount.text;
        string password = inputPassword.text;

        if (!string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(password))
        {
            // 发送网络请求，验证登录
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.ReqLogin,
                reqLogin = new ReqLogin
                {
                    acct = account,
                    pass = password
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
        else
        {
            GameRootResources.Instance().ShowTips("账号或密码为空");
        }
    }

    #endregion


    #region GM Test

    public void ClickGMBtn()
    {
        SetWindowState(false);
        GMSystem.Instance.StartSimulate();
    }

    #endregion
}
