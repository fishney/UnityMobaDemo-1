/****************************************************
    文件：WindowBase.cs
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
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowBase : MonoBehaviour
{
    protected ResSvc resSvc = null;
    protected AudioSvc audioSvc = null;
    protected NetSvc netSvc = null;
    //protected TimerSvc timerSvc = null;
    
    public void SetWindowState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }

        if (isActive)
        {
            InitWindow();
        }
        else
        {
            ClearWindow();
        }
    }

    protected virtual void InitWindow()
    {
        resSvc = ResSvc.Instance();
        audioSvc = AudioSvc.Instance();
        netSvc = NetSvc.Instance();
        //timerSvc = TimerSvc.Instance();
    }
    
    protected virtual void ClearWindow()
    {
        // resSvc = null;
        // audioSvc = null;
        // netSvc = null;
        // timerSvc = null;
    }

    protected void ShowTips(string tips)
    {
        GameRootResources.Instance().ShowTips(tips);
    }
    
    #region Tool Functions

    protected void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }

    protected void SetActive(Transform tf, bool isActive = true)
    {
        tf.gameObject.SetActive(isActive);
    }
    
    protected void SetActive(RectTransform rtf, bool isActive = true)
    {
        rtf.gameObject.SetActive(isActive);
    }
    
    protected void SetActive(Image img, bool isActive = true)
    {
        img.gameObject.SetActive(isActive);
    }
    
    protected void SetActive(Text txt, bool isActive = true)
    {
        txt.gameObject.SetActive(isActive);
    }

    protected void SetActive(InputField ipt, bool isActive = true)
    {
        ipt.gameObject.SetActive(isActive);
    }

    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
    
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }
    
    protected void SetText(Transform tf, string context = "")
    {
        SetText(tf.GetComponent<Text>(),context);
    }
    
    protected void SetText(Transform tf, int num = 0)
    {
        SetText(tf.GetComponent<Text>(),num);
    }

    protected T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }

        return t;
    }

    protected void SetSprite(Image img,string path)
    {
        Sprite sp = resSvc.LoadSprite(path, true);
        img.sprite = sp;
    }
    
    protected Transform GetTrans(GameObject go,string name)
    {
        return go.transform.Find(name).transform;
    }
    
    protected Transform GetTrans(Transform trans,string name)
    {
        return trans.Find(name).transform;
    }
    
    
    protected Image GetImage(Transform trans,string name = null)
    {
        if (name != null)
        {
            return trans.Find(name).GetComponent<Image>();
        }
        return trans.GetComponent<Image>();
    }
    
    protected Text GetText(Transform trans,string name = null)
    {
        if (name != null)
        {
            return trans.Find(name).GetComponent<Text>();
        }
        return trans.GetComponent<Text>();
    }
    
    #endregion
    
    #region Click Event
    
    protected void OnClickDown(GameObject go,Action<PointerEventData,object[]> action,params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickDown = action;
        listener.args = args;
    }
    
    protected void OnClickUp(GameObject go,Action<PointerEventData,object[]> action,params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickUp = action;
        listener.args = args;
    }
    
    protected void OnDrag(GameObject go,Action<PointerEventData,object[]> action,params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onDrag = action;
        listener.args = args;
    }

    protected void OnClick(GameObject go,Action<PointerEventData,object[]> action,params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClick = action;
        listener.args = args;
    }
    #endregion

    #region Timer

    /// <summary>
    /// MonoTimer UI显示进度用定时器
    /// </summary>
    /// <param name="cbAction">循环调用cb：SetText</param>
    /// <param name="intervalTime">循环周期：1000ms</param>
    /// <param name="loopCount">循环次数：5回</param>
    /// <param name="prgAction">进度修改cb：fillAmount = ...</param>
    /// <param name="endAction">结束回调(1次)</param>
    /// <param name="delayTime">延迟时长</param>
    /// <returns></returns>
    protected MonoTimer CreateMonoTimer(
        Action<int> cbAction,
        float intervalTime,
        int loopCount = 1,
        Action<bool, float, float> prgAction = null,
        Action endAction = null,
        float delayTime = 0)
    {
        MonoTimer timer = new MonoTimer(cbAction, intervalTime, loopCount, prgAction, endAction, delayTime);
        GameRoot.Instance().AddMonoTimer(timer);
        return timer;
    }

    #endregion
    
}
