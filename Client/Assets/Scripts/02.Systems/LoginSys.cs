/****************************************************
    文件：LoginSys.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using proto.HOKProtocol;
using UnityEngine;

public class LoginSys : SystemBase
{
    public static LoginSys Instance;

    public override void InitSys()
    {
        base.InitSys();
        
        Instance = this;
        this.Log("LoginSys Init Completed.");
    }
    
    /// <summary>
    /// 异步加载登陆场景和进度条，加载完成后再显示
    /// </summary>
    public void EnterLogin()
    {
        //resSvc.AsyncLoadScene(Constants.SceneLogin,OpenLoginWindow);
        gameRootResources.loginWindow.SetWindowState();
        audioSvc.PlayBGMusic(Constants.BGMainCity);
        gameRootResources.ShowTips("加载音乐资源...成功");
        gameRootResources.ShowTips("加载动画资源...成功");
    }
    
    /// <summary>
    /// 登录成功后执行，进入创建人物界面
    /// </summary>
    public void RspLogin(GameMsg msg)
    {
        gameRootResources.ShowTips("登陆成功");
        GameRoot.Instance().SetPlayerData(msg.rspLogin.playerData);

        if (string.IsNullOrEmpty(msg.rspLogin.playerData.name))
        {
            // TODO 新账号 改名页面
            //gameRootResources.createWindow.SetWindowState(true);
        }
        else
        {
            // TODO 进入下一页面
            gameRootResources.startWindow.SetWindowState(true);
            gameRootResources.loginWindow.SetWindowState(false);
            //MainCitySys.Instance.EnterMainCity();
        }

        // 关闭登录页面
        // gameRootResources.loginWindow.SetWindowState(false);
    }

    public void EnterLobby()
    {
        gameRootResources.startWindow.SetWindowState(false);
        LobbySys.Instance.EnterLobby();
    }
}
