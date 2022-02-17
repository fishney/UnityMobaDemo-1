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
using CodingK.UI;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class BagWindow : WindowBase
{
    public Transform canvas;
    public RectTransform bagPanel;
    public RectTransform scrollView;
    public RectTransform content;
    public CodingK_SV<ItemCfg, BagItem> customSV;
    public GameObject bagItem;
    private bool isShowing = false;
    public int poolCapacity = 80;

    // 取资源，应该放在Res里做，然后这里只赋值list就行
    protected void Start()
    {
        Init();
    }

    protected override void InitWindow()
    {
        base.InitWindow();
        
        ShowPanel();
    }

    private void Update()
    {
        if (customSV!= null && customSV.isShowing)
        {
            customSV.Tick();
        }
    }
    
    public void Init()
    {
        InitItemInfo();

        // bagPanel.transform.SetParent(canvas);
        // bagPanel.transform.localScale = Vector3.one;
        // bagPanel.transform.localPosition = Vector3.zero;
        // bagPanel.gameObject.SetActive(false);
    }
    
    public List<ItemCfg> items = new List<ItemCfg>();

    private void InitItemInfo()
    {
        for (int i = 0;i < 100;i++)
        {
            items.Add(new ItemCfg()
            {
                id = i,
                num = i,
            });
        }
    }
    
    public void ShowPanel()
    {
        customSV = new CodingK_SV<ItemCfg, BagItem>(items, content, scrollView,
            bagItem,poolCapacity);
        //customSV.InitItemView(100,100,45,25,5);
        customSV.InitItemView(30,30,50,75);
        
        bagPanel.gameObject.SetActive(true);
        
        customSV.Show();
        isShowing = true;
    }

    public void HidePanel()
    {
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
}

/// <summary>
/// 道具信息
/// </summary>
public class ItemCfg
{
    public int id;
    public int num;
    public string path;
}
