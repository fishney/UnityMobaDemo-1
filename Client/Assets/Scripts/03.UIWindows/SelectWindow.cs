using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄选择界面
/// </summary>
public class SelectWindow : WindowBase
{
    #region define
    
    public Image imgHeroShow;
    public Text txtCountTime;
    public Transform transScrollRoot;
    public GameObject heroItem;
    public Button btnSure;
    public Transform transSkillIconRoot;

    private int timeCount;
    
    /// <summary>
    /// 玩家可选择的英雄列表
    /// </summary>
    private List<HeroSelectData> heroSelectList;
    private bool isSelected;
   

    #endregion
    
    protected override void InitWindow()
    {
        base.InitWindow();

        btnSure.interactable = true;
        isSelected = false;
        timeCount = ServerConfig.SelectCountDown;
        heroSelectList = GameRoot.Instance().PlayerData.heroSelectData;

        for (int i = 0;i < transScrollRoot.childCount; i++)
        {
            DestroyImmediate(transScrollRoot.GetChild(i).gameObject);
        }

        // 英雄列表显示
        for (int i = 0; i < heroSelectList.Count; i++)
        {
            int heroId = heroSelectList[i].heroID;
            var go = Instantiate(heroItem);
            go.name = heroId.ToString();
            var rect = go.GetComponent<RectTransform>();
            rect.SetParent(transScrollRoot);
            rect.localScale = Vector3.one;
        }
        
        
    }
    
    
    
}
