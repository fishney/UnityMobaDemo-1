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
using cfg.Datas;
using CodingK.UI;
using HOK.Expansion;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class BagWindow : WindowBase
{
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
    public ItemInfo selectedItemInfo = null;

    private bool isWaitingCB = false;
    #endregion

    protected override void InitWindow()
    {
        base.InitWindow();
        selectedItemInfo = null;
        isWaitingCB = false;
        
        InitItemInfo();

        ShowPanel();
    }

    protected override void ClearWindow()
    {
        customSV = null;
        selectedItemInfo = null;
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
        
        // TODO 根据玩家背包数据，拼接对象
        for (int i = 0; i < 100; i++)
        {
            var itemCfg = resSvc.GetItemCfgById(i);
            if (itemCfg != null)
            {
                items.Add(new ItemInfo()
                {
                    id = i,
                    num = i,
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
        if (isWaitingCB)
        {
            ShowTips("等待服务器响应中，请检查网络状态！");
            return;
        }
        
        bagPanel.gameObject.SetActive(false);
        customSV.Destroy();
        customSV = null;
        isShowing = false;
        SetWindowState(false);
    }

    public void ClickSwitch()
    {
        if (isShowing)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }

    public void UpdateSelectedItemPanel(ItemInfo info)
    {
        // foreach (var bagItem in customSV.showingItems.Values)
        // {
        //     if (bagItem.info.id == info.id)
        //     {
        //         bagItem.SetSelectedFrame(true);
        //     }
        //     else
        //     {
        //         bagItem.SetSelectedFrame(false);
        //         bagItem.info.isSelected = false;
        //     }
        // }

        // 如果已经选中，就修正画面显示中其他被选择的状态
        if (lastSelectingItemId != -1)
        {
            if (customSV.showingItems.TryGetValue(lastSelectingItemId, out var tmpNotSelectedBagItem))
            {
                tmpNotSelectedBagItem.SetSelectedFrame(false);
                tmpNotSelectedBagItem.info.isSelected = false;
            }
        }

        lastSelectingItemId = info.id;
        
        if (customSV.showingItems.TryGetValue(lastSelectingItemId, out var tmpSelectedBagItem))
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
        if (selectedItemInfo == null || selectedItemInfo.num < 1)
        {
            ShowTips("所使用物品已用完! 请重新选择。");
            return;
        }

        isWaitingCB = true;
        // TODO request
        
    }

    public void RspUseItem()
    {
        isWaitingCB = false;
        // TODO update by response

        // 是否成功失败，失败提示，成功返回
        if (false)
        {
            ShowTips("物品使用失败！");
            // 更新本地数据。
        }
        else
        {
            // 更新本地数据。
            
            ShowTips("物品使用成功！");
        }
        
        
    }
}

