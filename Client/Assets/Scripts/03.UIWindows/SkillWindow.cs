using HOKProtocol;
using UnityEngine;

public partial class PlayWindow
{
    public SkillItem skaItem;
    public SkillItem sk1Item;
    public SkillItem sk2Item;
    public SkillItem sk3Item;

    public Transform imgInfoRoot;
    /// <summary>
    /// 禁止释放所有技能
    /// </summary>
    private bool isForbidAllSkill;
    
    public void InitSKillInfo()
    {
        BattleHeroData self = GameRoot.battleHeroList[GameRoot.SelfPosIndex];
        UnitCfg heroCfg = resSvc.GetUnitConfigById(self.heroId);
        int[] skillArr = heroCfg.skillArr;
        skaItem.InitSkillItem(resSvc.GetSkillConfigById(skillArr[0]),0);
        sk1Item.InitSkillItem(resSvc.GetSkillConfigById(skillArr[1]),1);
        sk2Item.InitSkillItem(resSvc.GetSkillConfigById(skillArr[2]),2);
        sk3Item.InitSkillItem(resSvc.GetSkillConfigById(skillArr[3]),3);

        SetAllSkillForbidState(false);
        SetActive(imgInfoRoot,false);
    }

    public void SetAllSkillForbidState()
    {
        SetAllSkillForbidState(true);
        isForbidAllSkill = true;
    }
    
    void UpdateSkillWnd() {
        if(isForbidAllSkill) {
            if(BattleSys.Instance.IsForbidAllSkill_SelfPlayer() == false) {
                SetAllSkillForbidState(false);
                isForbidAllSkill = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            skaItem.ClickSkillItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            sk1Item.ClickSkillItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            sk2Item.ClickSkillItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            sk3Item.ClickSkillItem();
        }
    }


    private void SetAllSkillForbidState(bool state)
    {
        sk1Item.SetForbidState(state);
        sk2Item.SetForbidState(state);
        sk3Item.SetForbidState(state);
    }
}
    
