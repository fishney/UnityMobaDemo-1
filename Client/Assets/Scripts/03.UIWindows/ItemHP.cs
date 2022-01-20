using System;
using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 血条显示
/// </summary>
public abstract class ItemHP : WindowBase
{
    public RectTransform rect;
    public Image imgPrg;// 血条进度图标
    
    protected bool isFriend;
    protected Transform rootTrans;
    protected int hpVal;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="unit">逻辑单元</param>
    /// <param name="root">需要映射的物体的位置</param>
    /// <param name="hp">初始化血量</param>
    public virtual void InitItem(MainLogicUnit unit, Transform root, int hp)
    {
        base.InitWindow();
        
        TeamEnum selfTeam = BattleSys.Instance.GetCurrentUserTeam();
        isFriend = unit.IsTeam(selfTeam);

        imgPrg.fillAmount = 1;
        rootTrans = root;
        hpVal = hp;
    }

    /// <summary>
    /// 更新血量
    /// </summary>
    public void UpdateHPPrg(int newVal)
    {
        if (newVal <= 0)
        {
            SetActive(gameObject,false);
        }
        else
        {
            SetActive(gameObject);
        }

        imgPrg.fillAmount = newVal * 1.0f / hpVal;
    }

    public virtual void SetStateInfo(StateEnum state, bool show)
    {
        
    }
    
    class JumpPack {
        public JumpNum jn;
        public JumpUpdateInfo jui;
    }
    Queue<JumpPack> jpq = new Queue<JumpPack>();
    public void AddHPJumpNum(JumpNum jn, JumpUpdateInfo jui) {
        jpq.Enqueue(new JumpPack { jn = jn, jui = jui });
    }

    public float JumpNumInterval;
    float JumpNumCounter = 0;
    bool canShow = true;
    float delayTimeCounter = 0;
    public void UpdateCheck() {
        // if(jpq.Count > 0 && canShow) {
        //     if (JumpNumCounter < JumpNumInterval)
        //     {
        //         JumpNumCounter += Configs.ServerLogicFrameIntervelMs;
        //     }
        //     else
        //     {
        //         JumpNumCounter -= JumpNumInterval;
        //         canShow = false;
        //         JumpPack jp = jpq.Dequeue();
        //         jp.jn.Show(jp.jui);
        //     }
        // }
        
        if(jpq.Count > 0 && canShow) {
            JumpPack jp = jpq.Dequeue();
            jp.jn.Show(jp.jui);
        }


        if(canShow == false) {
            delayTimeCounter += Time.deltaTime;
            if(delayTimeCounter > JumpNumInterval / 1000) {
                delayTimeCounter = 0;
                canShow = true;
            }
        }

        if(rootTrans) {
            float scaleRate = 1.0F * ClientConfig.ScreenStandardHeight / Screen.height;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(rootTrans.position);
            rect.anchoredPosition = screenPos * scaleRate;
        }
    }

    IEnumerator Show(JumpPack jp)
    {
        yield return new WaitForSeconds(3);
        jp.jn.Show(jp.jui);
    }
}

/// <summary>
/// 人物状态
/// </summary>
public enum StateEnum
{
    None,
    Silenced,//沉默
    Knockup,//击飞
    Stunned,//眩晕
    
    Invincible,//无敌
    Restricted,//禁锢
}