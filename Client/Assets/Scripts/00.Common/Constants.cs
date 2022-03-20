/****************************************************
    文件：Constants.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    #region 音效名称

    // 登录bgm
    //public const string BGLogin = "bgLogin";
    // 主城bgm
    public const string BGMainCity = "main";
    public const string BGLobby = "lobby";
    // 战斗bgm
    public const string BGBattle = "battle";

    public const string LoginBtn = "LoginBtnClick";


    #endregion
    
    #region 资源路径
    
    public const string IconPath = "ResImages/MatchWnd/icon_";
 
    public const string IconFramePath = "ResImages/MatchWnd/frame_";


    #endregion
}

public static class ClientConfig {
    public const int ScreenStandardWidth = 2160;
    public const int ScreenStandardHeight = 1080;

    public const int ScreenOPDis = 135;
    public const int SkillOPDis = 125;
    public const int SkillCancelDis = 500;

    public const int CommonMoveAttackBuffId = 90000;
}