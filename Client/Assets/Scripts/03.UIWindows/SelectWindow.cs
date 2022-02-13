using System.Collections;
using System.Collections.Generic;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private int selectHeroId;
   

    #endregion
    
    protected override void InitWindow()
    {
        base.InitWindow();

        btnSure.interactable = true;
        isSelected = false;
        timeCount = ServerConfig.SelectCountDown;
        heroSelectList = GameRoot.Instance().PlayerData.heroSelectData;

        for (int i = transScrollRoot.childCount - 1;i >= 0; --i)
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
            UnitCfg cfg = resSvc.GetUnitConfigById(heroId);
            SetSprite(GetImage(go.transform,"imgIcon"),"ResImages/SelectWnd/" + cfg.resName + "_head");
            SetText(GetText(go.transform,"txtName"),cfg.unitName);
            
            OnClick(go,ClickHeroItem,go,heroId);
            
            if (i == 0)
            {
                ClickHeroItem(null,new object[]{go,heroId});
            }
        }

        
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
            
           
            if (timeCount == 0 && !isSelected)
            {
                ClickSureBtn();
            }
            
            int min = timeCount / 60;
            int sec = timeCount % 60;
            // 进行一下补全,9补成09
            string minStr = min < 10 ? "0" + min + ":" : min + ":";
            string secStr = sec < 10 ? "0" + sec : sec.ToString();
        
            SetText(txtCountTime,minStr + secStr);
        }
    }

    /// <summary>
    /// 选择英雄事件
    /// </summary>
    void ClickHeroItem(PointerEventData pd,object[] args)
    {
        audioSvc.PlayUIAudio("selectHeroClick");
        if (isSelected)
        {
            GameRootResources.Instance().ShowTips("已经确定选择了英雄!");
            return;
        }

        var go = args[0] as GameObject;

        // 遍历修改英雄选择框
        for (int i = 0; i < transScrollRoot.childCount; i++)
        {
            Transform item = transScrollRoot.GetChild(i);
            Image selectGlow = GetImage(item, "state");
            if (item.gameObject.Equals(go))
            {
                SetSprite(selectGlow,"ResImages/SelectWnd/selectGlow");
            }
            else
            {
                SetSprite(selectGlow,"ResImages/MatchWnd/frame_normal");
            }
        }

        // arg: heroId
        selectHeroId = (int) args[1];
        UnitCfg cfg = resSvc.GetUnitConfigById(selectHeroId);
        SetSprite(imgHeroShow,"ResImages/SelectWnd/"+cfg.resName+"_show");

        // 修改技能显示
        for (int i = 0; i < transSkillIconRoot.childCount; i++)
        {
            Image icon = GetImage(transSkillIconRoot.GetChild(i));
            SetSprite(icon,"ResImages/PlayWnd/"+cfg.resName+"_sk"+i);
        }
    }

    public void ClickSureBtn()
    {
        audioSvc.PlayUIAudio("com_click2");

        if (isSelected)
        {
            return;
        }

        GameMsg msg = new GameMsg()
        {
            cmd = CMD.SendSelect,
            sendSelect = new SendSelect()
            {
                heroId = selectHeroId,
                roomId = GameRoot.ActiveRoomId,
            }
        };
        
        netSvc.SendMsg(msg);

        btnSure.interactable = false;
        isSelected = true;
    }
    
}
