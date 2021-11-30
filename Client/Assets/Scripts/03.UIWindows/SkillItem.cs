
using System.Collections;
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

    private int skillIndex;
    private SkillCfg skillCfg;
    private float pointDis;
    private Vector2 startPos = Vector2.zero;
    
    public void InitSkillItem(SkillCfg skillCfg,int skillIndex)
    {
        SetActive(EffectRoot,false);
        InitWindow();

        this.skillIndex = skillIndex;
        this.skillCfg = skillCfg;

        pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.SkillOPDis;
        
        if (skillCfg.isNormalAttack == false)
        {
            
        }
        else
        {
            // 普攻
            OnClickDown(skillIcon.gameObject, (evt, args) =>
            {
                // TODO show Range
                ClickSkillItem();
            });
            
            OnClickUp(skillIcon.gameObject, (evt, args) =>
            {
                // TODO disable Range
                ShowEffect();
            });
        }
    }

    private Coroutine ct = null;
    /// <summary>
    /// 如果已经激活过了，就重新激活
    /// </summary>
    void ShowEffect()
    {
        if (ct != null)
        {
            StopCoroutine(ct);
            SetActive(EffectRoot,false);
        }
        SetActive(EffectRoot);
        ct = StartCoroutine(DisableEffect());
    }

    IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(0.5f);
        SetActive(EffectRoot,false);
    }

    public void ClickSkillItem()
    {
        BattleSys.Instance.SendSkillKey(skillCfg.skillId,Vector3.zero);
    }
    
    /// 非指向技能
    public void ClickSkillItem(Vector3 vec)
    {
        BattleSys.Instance.SendSkillKey(skillCfg.skillId,vec);
    }
    
    public void SetForbidState(bool state)
    {
        SetActive(imgForbid);
    }
}