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
using UnityEngine;

public class LoginSys : SystemBase
{
    public static LoginSys Instance;

    public void InitSys()
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
        //gameRootResources.ShowTips("加载音乐资源...成功");
        //gameRootResources.ShowTips("加载动画资源...成功");
    }
}
