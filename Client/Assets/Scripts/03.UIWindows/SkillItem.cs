
using UnityEngine;
using UnityEngine.UI;

/// 技能Button
public class SkillItem : WindowBase
{
    public Image imgCycle;
    public Image skillIcon;
    public Image imgCd;
    public Text txtCD;
    public Image imgPoint;
    public Image imgForbid;
    public Transform EffectRoot;
    
    public void InitSkillItem(SkillCfg skillCfg,int skillIndex)
    {
        SetActive(EffectRoot,false);
        InitWindow();
    }

    
    
    
    
    public void SetForbidState(bool state)
    {
        SetActive(imgForbid);
    }
}