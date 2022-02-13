/****************************************************
    文件：LoadWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class LoadWindow : WindowBase
{
    #region define

    public Transform blueTeamRoot;
    public Transform redTeamRoot;

    private List<BattleHeroData> battleHeroList;
    
    /// <summary>
    /// 用于记录本地英雄进度,刷新
    /// </summary>
    private List<Text> txtPercentList;

    #endregion
    
    protected override void InitWindow()
    {
        base.InitWindow();
        audioSvc.PlayUIAudio("load");
        battleHeroList = GameRoot.battleHeroList;
        txtPercentList = new List<Text>();

        int count = battleHeroList.Count / 2;

        // blue team
        for (int i = 0; i < 5; i++)
        {
            Transform player = blueTeamRoot.GetChild(i);
            if (i < count)
            {
                SetActive(player);
                UnitCfg cfg = resSvc.GetUnitConfigById(battleHeroList[i].heroId);
                SetSprite(GetImage(player,"imgHero"),"ResImages/LoadWnd/" + cfg.resName + "_load");
                SetText(GetText(player,"txtHeroName"),cfg.unitName);
                SetText(GetText(player,"bgName/txtPlayerName"),battleHeroList[i].userName);
                Text txtPrg = GetText(player, "txtProgress");
                txtPercentList.Add(txtPrg);
                SetText(txtPrg,"0%");
            }
            else
            {
                SetActive(player,false);
            }
        }
        
        // red team
        for (int i = 0; i < 5; i++)
        {
            Transform player = redTeamRoot.GetChild(i);
            if (i < count)
            {
                SetActive(player);
                UnitCfg cfg = resSvc.GetUnitConfigById(battleHeroList[i + count].heroId);
                SetSprite(GetImage(player,"imgHero"),"ResImages/LoadWnd/" + cfg.resName + "_load");
                SetText(GetText(player,"txtHeroName"),cfg.unitName);
                SetText(GetText(player,"bgName/txtPlayerName"),battleHeroList[i + count].userName);
                Text txtPrg = GetText(player, "txtProgress");
                txtPercentList.Add(txtPrg);
                SetText(txtPrg,"0%");
            }
            else
            {
                SetActive(player,false);
            }
        }
        
    }

    public void RefreshPrgData(List<int> percentList)
    {
        for (int i = 0; i < percentList.Count; i++)
        {
            txtPercentList[i].text = percentList[i] + "%";
        }
    }
    
    
}
