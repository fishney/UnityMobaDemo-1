/****************************************************
    文件：TipsWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsWindow : WindowBase
{
    public Animator tipsAni;
    public Text txtTips;
    public Image imgTips;
    private bool isTipsShow = false;
    private Queue<string> tipsPool = new Queue<string>();
    
    protected override void InitWindow()
    {
        base.InitWindow();
        SetActive(imgTips,false);
    }

    private void Update()
    {
        if (tipsPool?.Count > 0 && !isTipsShow)
        {
            lock (tipsPool)
            {
                SetTips(tipsPool.Dequeue());
                isTipsShow = true;
            }
            
        }
    }

    #region Tips相关

    public void AddTips(string tip)
    {
        lock (tipsPool)
        {
            tipsPool.Enqueue(tip);
        }
    }
    
    private void SetTips(string tips)
    {
        SetActive(imgTips,true);
        SetText(txtTips,tips);
        
        tipsAni.Play("TipsWindow",0,0);
        
        // 延时关闭激活状态
        // StartCoroutine(AniPlayDone(clip.length, () =>
        // {
        //     SetActive(imgTips,false);
        //     isTipsShow = false;
        // }));
    }

    // private IEnumerator AniPlayDone(float sec, Action action)
    // {
    //     yield return new WaitForSeconds(sec);
    //     if (action!=null)
    //     {
    //         action();
    //     }
    // }
    
    public void AniPlayDone()
    {
        SetActive(imgTips,false);
        isTipsShow = false;
    }
    
    #endregion
    
    
}
