
using System.Collections;
using HOKProtocol;
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

    private HeroView viewHero;

    private int skillIndex;
    private SkillCfg skillCfg;
    /// 根据宽高比算出的最远技能点拖拽距离
    private float pointDis;
    private Vector2 startPos = Vector2.zero;
    
    public void InitSkillItem(SkillCfg skillCfg,int skillIndex)
    {
        SetActive(EffectRoot,false);
        InitWindow();

        viewHero = BattleSys.Instance.GetSelfHero().mainViewUnit as HeroView;

        this.skillIndex = skillIndex;
        this.skillCfg = skillCfg;

        pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.SkillOPDis;
        
        if (skillCfg.isNormalAttack == false)
        {
            SetSprite(skillIcon,"ResImages/PlayWnd/" + skillCfg.iconName);
            SetActive(imgCd,false);
            SetActive(txtCD,false);
            
            OnClickDown(skillIcon.gameObject, (evt, args) =>
            {
                startPos = evt.position;
                // 显示技能托盘与拖点
                SetActive(imgCycle,true);
                SetActive(imgPoint,true);
                ShowSkillAtkRange(true);
                if (skillCfg.releaseMode == ReleaseModeEnum.Position)
                {
                    // 通知场景中显示skill guide
                    viewHero.SetSkillGuide(skillIndex,true,ReleaseModeEnum.Position,Vector3.zero);
                }
                else if (skillCfg.releaseMode == ReleaseModeEnum.Direction)
                {
                    viewHero.SetSkillGuide(skillIndex,true,ReleaseModeEnum.Direction,Vector3.zero);
                }
            });
            
            OnDrag(skillIcon.gameObject, (evt, args) =>
            {
                #region UI映射
                
                Vector2 dir = evt.position - startPos;
                float len = dir.magnitude;
                if (len > pointDis)
                {
                    // 拖拽距离
                    Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                    imgPoint.transform.position = startPos + clampDir;
                }
                else
                {
                    imgPoint.transform.position = evt.position;
                }
                
                #endregion

                #region 模型映射

                if (skillCfg.releaseMode == ReleaseModeEnum.Position)
                {
                    if (dir == Vector2.zero)
                    {
                        return;
                    }
                    // 从UI映射到地图
                    dir = BattleSys.Instance.SkillDisMultiplier * dir;
                    Vector2 clampDir = Vector2.ClampMagnitude(dir, skillCfg.targetCfg.selectRange);
                    Vector3 clampDirVector3 = new Vector3(clampDir.x, 0, clampDir.y);
                    clampDirVector3 = Quaternion.Euler(0, 45, 0) * clampDirVector3;// 这里的45度是相机偏移的45度
                    viewHero.SetSkillGuide(skillIndex,true,ReleaseModeEnum.Position,clampDirVector3);
                }
                else if (skillCfg.releaseMode == ReleaseModeEnum.Direction)
                {
                    Vector3 dirVector3 = new Vector3(dir.x, 0, dir.y);
                    dirVector3 = Quaternion.Euler(0, 45, 0) * dirVector3;// 这里的45度是相机偏移的45度
                    viewHero.SetSkillGuide(skillIndex,true,ReleaseModeEnum.Direction,dirVector3.normalized);
                }
                else
                {
                    this.Warn(skillCfg.releaseMode.ToString() + " 没有实现！");
                }

                // 显示技能取消
                if (len >= ClientConfig.SkillCancelDis)
                {
                    SetActive(GameRootResources.Instance().playWindow.imgCancelSkill);
                }
                else
                {
                    SetActive(GameRootResources.Instance().playWindow.imgCancelSkill,false);
                }
                #endregion
            });
            
            OnClickUp(skillIcon.gameObject, (evt, args) =>
            {
                Vector2 dir = evt.position = startPos;
                imgPoint.transform.position = startPos;
                SetActive(imgCycle,false);
                SetActive(imgPoint,false);
                
                SetActive(GameRootResources.Instance().playWindow.imgCancelSkill,false);
                ShowSkillAtkRange(false);

                if (dir.magnitude >= ClientConfig.SkillCancelDis)
                {
                    viewHero.DisableSkillGuide(skillIndex);
                    return;
                }

                if (skillCfg.releaseMode == ReleaseModeEnum.Click)
                {
                    // 直接释放技能
                    viewHero.DisableSkillGuide(skillIndex);
                    ClickSkillItem();
                }
                else if (skillCfg.releaseMode == ReleaseModeEnum.Position)
                {
                    viewHero.DisableSkillGuide(skillIndex);
                    dir = BattleSys.Instance.SkillDisMultiplier * dir;
                    Vector2 clampDir = Vector2.ClampMagnitude(dir, skillCfg.targetCfg.selectRange);
                    Vector3 clampDirVector3 = new Vector3(clampDir.x, 0, clampDir.y);
                    clampDirVector3 = Quaternion.Euler(0, 45, 0) * clampDirVector3;// 这里的45度是相机偏移的45度
                    ClickSkillItem(clampDirVector3);
                }
                else if (skillCfg.releaseMode == ReleaseModeEnum.Direction)
                {
                    viewHero.DisableSkillGuide(skillIndex);
                    if (dir == Vector2.zero)
                    {
                        return;
                    }
                    
                    Vector3 dirVector3 = new Vector3(dir.x, 0, dir.y);
                    dirVector3 = Quaternion.Euler(0, 45, 0) * dirVector3;// 这里的45度是相机偏移的45度
                    ClickSkillItem(dirVector3);
                }
                else
                {
                    this.Warn("Skill release mode not exist.");
                }
                
                ShowEffect();
            });
            
        }
        else
        {
            // 普攻
            OnClickDown(skillIcon.gameObject, (evt, args) =>
            {
                ShowSkillAtkRange(true);
                ClickSkillItem();
            });
            
            OnClickUp(skillIcon.gameObject, (evt, args) =>
            {
                ShowSkillAtkRange(false);
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

    private void ShowSkillAtkRange(bool state)
    {
        if (skillCfg.targetCfg != null)
        {
            viewHero.SetAtkSkillRange(state,skillCfg.targetCfg.selectRange);
        }
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