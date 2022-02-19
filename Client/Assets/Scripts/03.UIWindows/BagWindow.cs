/****************************************************
    文件：LoadWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cfg.Datas;
using cfg.Enums;
using CodingK.UI;
using HOK.Expansion;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class BagWindow : WindowBase
{
    #region top bar

    public Text txtName;
    public Text txtLevel;
    public Text txtExp;
    public Image fgExp;
    public Text txtCoin;
    public Text txtDiamond;
    public Text txtTicket;

    #endregion
    
    public Transform canvas;
    public RectTransform bagPanel;
    public RectTransform scrollView;
    public RectTransform content;
    public CodingK_SV<ItemInfo, BagItem> customSV;
    public GameObject bagItem;
    private bool isShowing = false;
    public int poolCapacity = 80;
    private int lastSelectingItemId = -1;

    #region Selected Item

    public Text selectedItemName;
    public Text selectedItemNum;
    public Image selectedItemImg;
    public Text selectedItemDes;

    private int waitingCBCount = 0;
    private List<int> usedItemList;
    #endregion

    protected override void InitWindow()
    {
        base.InitWindow();
        lastSelectingItemId = -1;
        waitingCBCount = 0;

        RefreshPlayData();
        InitItemInfo();

        ShowPanel();
    }

    protected override void ClearWindow()
    {
        customSV = null;
        lastSelectingItemId = -1;
        items.Clear();
        items = null;
    }

    private void Update()
    {
        if (customSV!= null && customSV.isShowing)
        {
            customSV.Tick();
        }
    }

    public List<ItemInfo> items = new List<ItemInfo>();

    private void InitItemInfo()
    {
        items = new List<ItemInfo>();
        
        // 根据玩家背包数据，拼接对象
        foreach (var bagItemData in GameRoot.Instance().PlayerData.bagData)
        {
            if (bagItemData.itemNum < 1)
            {
                continue;
            }
            
            var itemCfg = resSvc.GetItemCfgById(bagItemData.itemId);
            if (itemCfg != null )
            {
                items.Add(new ItemInfo()
                {
                    id = bagItemData.itemId,
                    num = bagItemData.itemNum,
                    isSelected = false,
                    cfg = itemCfg,
                });
            }
        }
    }
    
    public void ShowPanel()
    {
        customSV = new CodingK_SV<ItemInfo, BagItem>(items, content, scrollView,
            bagItem,poolCapacity);
        //customSV.InitItemView(100,100,45,25,5);
        customSV.InitItemView(30,30,50,75);
        
        bagPanel.gameObject.SetActive(true);
        
        customSV.Show();
        isShowing = true;
    }

    public void HidePanel()
    {
        if (waitingCBCount > 0)
        {
            ShowTips("等待服务器响应中，请检查网络状态！");
            return;
        }
        
        audioSvc.PlayUIAudio("com_click2");
        
        bagPanel.gameObject.SetActive(false);
        customSV.Destroy();
        customSV = null;
        isShowing = false;
        SetWindowState(false);
    }

    public void UpdateSelectedItemPanel(ItemInfo info)
    {
        audioSvc.PlayUIAudio("com_click1");
        
        // 如果已经选中，就修正画面显示中其他被选择的状态
        if (lastSelectingItemId != -1)
        {
            var tmpNotSelectedBagItem = customSV.showingItems.Values.FirstOrDefault(o=>o.info?.id == lastSelectingItemId);
            if (tmpNotSelectedBagItem != null)
            {
                tmpNotSelectedBagItem.SetSelectedFrame(false);
                tmpNotSelectedBagItem.info.isSelected = false;
            }
            else
            {
                var tmpNotSelectedBagItemMiss = items.FirstOrDefault(o => o.id == lastSelectingItemId);
                if (tmpNotSelectedBagItemMiss != null) tmpNotSelectedBagItemMiss.isSelected = false;
            }
        }

        lastSelectingItemId = info.id;
        
        var tmpSelectedBagItem = customSV.showingItems.Values.FirstOrDefault(o=>o.info?.id == lastSelectingItemId);
        
        if (tmpSelectedBagItem != null)
        {
            tmpSelectedBagItem.SetSelectedFrame(true);
        }

        info.isSelected = true;
        selectedItemName.text = info.cfg.name;
        selectedItemNum.text = info.num.ToString();
        selectedItemDes.text = info.cfg.des;
        //selectedItemImg.SetSprite(cfg.imgPath);
    }

    public void ClickUseButton()
    {
        audioSvc.PlayUIAudio("com_click1");
        if (waitingCBCount > 0)
        {
            ShowTips("使用物品太过频繁，请稍后。");
            return;
        }
        
        var itemInfo = items?.FirstOrDefault(o => o.id == lastSelectingItemId && o.isSelected);
        if (itemInfo == null || itemInfo.num < 1)
        {
            ShowTips("所使用物品已用完! 请重新选择。");
            return;
        }

        waitingCBCount++;
        // request
        GameMsg msg = new GameMsg {
            cmd = CMD.ReqBagItem,
            reqBagItem = new ReqBagItem() {
                itemId = itemInfo.id,
                itemNum = 1,
            }
        };
        netSvc.SendMsg(msg);
    }

    public void RefreshPlayData()
    {
        var pd = GameRoot.Instance().PlayerData;
        var maxExp = GameRoot.Instance().GetMaxExp(pd.level);
        SetText(txtName,pd.name);
        SetText(txtLevel,"Lv." + pd.level);
        SetText(txtExp,pd.exp + " / " + maxExp);
        SetText(txtCoin,pd.coin);
        SetText(txtDiamond,pd.diamond);
        SetText(txtTicket,pd.ticket);
        fgExp.fillAmount = 1.0f * pd.exp / maxExp;
    }

    /// <summary>
    /// PlayerData交给GameRoot中的广播解决，这里只负责弹出tips和更新字典
    /// </summary>
    public void RspUseItem(int usedItemId,bool success)
    {
        waitingCBCount--;
        
        // 是否成功失败，失败提示，成功返回
        if (success)
        {
            var usedItem = items.FirstOrDefault(o => o.id == usedItemId);
            var tip = new StringBuilder();
            tip.Append(usedItem.cfg.name + " 使用成功！");
            foreach (var effect in usedItem.cfg.effectList_Ref)
            {
                switch (effect.effectType)
                {
                    case ItemEffectType.Coin:
                        tip.Append(" 金币:" + effect.effectVal);
                        break;
                    case ItemEffectType.Exp:
                        tip.Append(" 经验:" + effect.effectVal);
                        break;
                    case ItemEffectType.Diamond:
                        tip.Append(" 钻石:" + effect.effectVal);
                        break;
                    case ItemEffectType.Ticket:
                        tip.Append(" 英雄卷:" + effect.effectVal);
                        break;
                }
            }

            if (--usedItem.num < 1)
            {
                usedItem.isSelected = false;
                customSV.RemoveData(usedItem);
            }
            
            
            ShowTips(tip.ToString());
            
            
        }
        else
        {
            ShowTips("物品使用失败！");
        }
    }
}

