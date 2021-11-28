using HOKProtocol;
using UnityEngine;

public partial class PlayWindow
{
    public SkillItem skaItem;
    public SkillItem sk1Item;
    public SkillItem sk2Item;
    public SkillItem sk3Item;

    public Transform imgInfoRoot;
    
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

    private void SetAllSkillForbidState(bool state)
    {
        sk1Item.SetForbidState(state);
        sk2Item.SetForbidState(state);
        sk3Item.SetForbidState(state);
    }
}
    
