using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayWindow
{
    public SkillItem skaItem;
    public SkillItem sk1Item;
    public SkillItem sk2Item;
    public SkillItem sk3Item;

    public Transform imgInfoRoot;
    public Image imgInfoCD;
    
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

        /*
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
        
        if(Input.GetKeyDown(KeyCode.Q)) {
            GameRoot.SelfPosIndex = 0;
        }
        else if(Input.GetKeyDown(KeyCode.W)) {
            GameRoot.SelfPosIndex = 1;
        }
        else if(Input.GetKeyDown(KeyCode.E)) {
            GameRoot.SelfPosIndex = 2;
        }
        else if(Input.GetKeyDown(KeyCode.R)) {
            GameRoot.SelfPosIndex = 3;
        }
        else if(Input.GetKeyDown(KeyCode.T)) {
            GameRoot.SelfPosIndex = 4;
        }
        else if(Input.GetKeyDown(KeyCode.Y)) {
            GameRoot.SelfPosIndex = 5;
        }
        */
    }


    private void SetAllSkillForbidState(bool state)
    {
        sk1Item.SetForbidState(state);
        sk2Item.SetForbidState(state);
        sk3Item.SetForbidState(state);
    }
    
    /// <summary>
    /// 被动buff小图标显示
    /// </summary>
    /// <param name="cdTime">持续时间</param>
    public void SetImgInfo(int cdTime) {
        SetActive(imgInfoRoot);
        showImgInfo = true;
        showTimeCounter = 0;
        showTime = cdTime * 1.0F / 1000;
    }
    
    bool showImgInfo;
    float showTimeCounter;
    float showTime;
    void UpdateImgInfo(float delta) {
        if(showImgInfo) {
            showTimeCounter += delta;
            if(showTimeCounter >= showTime) {
                showTimeCounter = 0;
                showImgInfo = false;
                SetActive(imgInfoRoot,false);
            }
            else {
                imgInfoCD.fillAmount = 1 - showTimeCounter / showTime;
            }
        }
    }
}
    
