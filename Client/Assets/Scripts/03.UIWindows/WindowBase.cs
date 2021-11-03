/****************************************************
    文件：WindowBase.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //Sprite sp = resSvc.LoadSprite(path, true);
        //img.sprite = sp;
    }
    
    protected Transform GetTrans(GameObject go,string name)
    {
        return go.transform.Find(name).transform;
    }
    
    protected Transform GetTrans(Transform trans,string name)
    {
        return trans.Find(name).transform;
    }
    #endregion
}
