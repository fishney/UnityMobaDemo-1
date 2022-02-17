/****************************************************
    文件：GameRootResources.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sth like WindowManager etc.
/// </summary>
public class GameRootResources : GameRootMonoSingleton<GameRootResources>
{
    // public LoadingWindow loadingWindow;
    //
    public LoginWindow loginWindow; 
    
    public TipsWindow tipsWindow;
    
    public StartWindow startWindow;
    
    public LobbyWindow lobbyWindow;
    
    public MatchWindow matchWindow;
    
    public SelectWindow selectWindow;
    
    public LoadWindow loadWindow;
    
    public PlayWindow playWindow;
    
    public HPWindow hpWindow;

    public ResultWindow resultWindow;
    
    public BagWindow bagWindow;
    
    
    public void ShowTips(string tip)
    {
        tipsWindow.AddTips(tip);
    }
}
