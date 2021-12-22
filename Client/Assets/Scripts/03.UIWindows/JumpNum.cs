using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 伤害飘字
/// </summary>
public class JumpNum : MonoBehaviour
{
    #region 参数
    
    public RectTransform rect;
    public Animator ani;
    public Text txt;

    public int MaxFont;// 最大字体，比如60
    public int MinFont;// 最小字体，比如40
    public int MaxFontValue;// 将伤害转化为字体大小的计算值x，举例：伤害刚好=x，字体大小就取50
    
    public Color skillDamageColor;
    public Color buffDamageColor;
    public Color cureDamageColor;
    public Color slowSpeedColor;
    
    #endregion

    #region 为缓存池公开

    private JumpNumPool ownerPool;

    public void Init(JumpNumPool ownerPool)
    {
        this.ownerPool = ownerPool;
    }

    /// <summary>
    /// 播放
    /// </summary>
    public void Show(JumpUpdateInfo ji)
    {
        int fontSize = (int) Mathf.Clamp(ji.jumpVal * 1.0f / MaxFontValue,MinFont,MaxFont);
        txt.fontSize = fontSize;
        rect.anchoredPosition = ji.pos;
        
        switch(ji.jumpType) {
            case JumpTypeEnum.SkillDamage:
                txt.text = ji.jumpVal.ToString();
                txt.color = skillDamageColor;
                break;
            case JumpTypeEnum.BuffDamage:
                txt.text = ji.jumpVal.ToString();
                txt.color = buffDamageColor;
                break;
            case JumpTypeEnum.Cure:
                txt.text = "+" + ji.jumpVal;
                txt.color = cureDamageColor;
                break;
            case JumpTypeEnum.SlowSpeed:
                txt.text = "减速";
                txt.color = slowSpeedColor;
                break;
            case JumpTypeEnum.None:
            default:
                break;
        }

        switch(ji.jumpAni) {
            case JumpAniEnum.LeftCurve:
                ani.Play("JumpLeft", 0);
                break;
            case JumpAniEnum.RightCurve:
                ani.Play("JumpRight", 0);
                break;
            case JumpAniEnum.CenterUp:
                ani.Play("JumpCenter", 0);
                break;
            case JumpAniEnum.None:
            default:
                break;
        }
        
        StartCoroutine(Recycle());
    }

    /// <summary>
    /// 回收
    /// </summary>
    IEnumerator Recycle()
    {
        yield return new WaitForSeconds(0.75f);
        ani.Play("Empty");// 播放空动画
        ownerPool.PushOne(this);
        
    }

    #endregion
}

public class JumpUpdateInfo {
    public int jumpVal;//数值
    public Vector2 pos;//位置
    public JumpTypeEnum jumpType;
    public JumpAniEnum jumpAni;
}

/// <summary>
/// 缓存池
/// </summary>
public class JumpNumPool
{
    private Transform poolRoot;
    private Queue<JumpNum> jumpNumQue;
    
    int _index = 0;
    int Index { get { return ++_index; } }
    
    public JumpNumPool(int count,Transform poolRoot)
    {
        this.poolRoot = poolRoot;
        jumpNumQue = new Queue<JumpNum>();
        for (int i = 0; i < count; i++)
        {
            PushOne(CreateOne());
        }
    }

    /// <summary>
    /// 创建、初始化并返回一个JumpNum预制体
    /// </summary>
    JumpNum CreateOne()
    {
        GameObject go = ResSvc.Instance().LoadPrefab("UIPrefab/DynamicItem/JumpNum");
        go.name = "JumpNum_" + Index;
        go.transform.SetParent(poolRoot);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        JumpNum jn = go.GetComponent<JumpNum>();
        jn.Init(this);
        return jn;
    }
    
    public JumpNum PopOne() {
        if(jumpNumQue.Count > 0) {
            return jumpNumQue.Dequeue();
        }
        else {
            this.Warn("飘字超额，动态调整上限");
            PushOne(CreateOne());
            return PopOne();
        }
    }

    public void PushOne(JumpNum jn) {
        jumpNumQue.Enqueue(jn);
    }
}

public enum JumpTypeEnum
{
    None,
    SkillDamage,
    BuffDamage,
    Cure,// 治疗
    SlowSpeed,// 减速
}

public enum JumpAniEnum
{
    None,
    LeftCurve,// 左曲线飘出
    RightCurve,// 右曲线飘出
    CenterUp,
}