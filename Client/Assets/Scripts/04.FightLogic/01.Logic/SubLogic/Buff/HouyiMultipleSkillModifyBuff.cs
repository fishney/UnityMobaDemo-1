using proto.HOKProtocol;
using PEMath;

public class HouyiMultipleSkillModifyBuffCfg : BuffCfg {
    public int originalID;
    public int powerID;
    public int superPowerID;
    public int triggerOverCount;
    public int resetTime;
}

/// <summary>
/// 后裔被动，强化普攻为3次连射
/// </summary>
public class HouyiMultipleSkillModifyBuff : Buff {
    int originalID;
    int powerID;// 3连射强化版普攻id
    int superPowerID;// 被动+技能混合的 3*3 强化普攻id

    Skill modifySkill;

    int currOverCount;
    int triggerOverCount;// 被动到达该层后就触发
    int resetTime;

    public HouyiMultipleSkillModifyBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        HouyiMultipleSkillModifyBuffCfg hpsmbc = cfg as HouyiMultipleSkillModifyBuffCfg;
        triggerOverCount = hpsmbc.triggerOverCount;
        originalID = hpsmbc.originalID;
        powerID = hpsmbc.powerID;
        superPowerID = hpsmbc.superPowerID;
        resetTime = hpsmbc.resetTime;
        modifySkill = owner.GetSkillByID(originalID);
        Skill[] skillArr = source.GetAllSkill();
        for(int i = 0; i < skillArr.Length; i++) {
            skillArr[i].SpellSuccessBp += OnSpellSkillSucc;
        }
    }

    void OnSpellSkillSucc(Skill skillReleased) {
        if(skillReleased.skillCfg.isNormalAttack) {
            timeCount = 0;
            if(currOverCount >= triggerOverCount) {
                owner.mainViewUnit.SetImgInfo(resetTime);
                return;
            }
            else {
                ++currOverCount;
                if(currOverCount == triggerOverCount) {
                    isCounter = true;
                    owner.mainViewUnit.SetImgInfo(resetTime);
                    if(modifySkill.TempSkillID == 0) {
                        // 如果触发被动时，没有处于技能1状态，就转换普攻为1*3连击
                        modifySkill.ReplaceSkillCfg(powerID);
                    }
                    else {
                        // 如果触发被动时，处于技能1状态，就转换普攻为3*3连击
                        modifySkill.ReplaceSkillCfg(superPowerID);
                    }
                }
            }
        }
        else {
            if(skillReleased.skillId != 1021) {
                ResetSkill();
            }
        }
    }

    int timeCount;
    bool isCounter;
    protected override void Tick() {
        base.Tick();
        if(isCounter) {
            timeCount += Configs.ServerLogicFrameIntervelMs;
            if(timeCount >= resetTime) {
                ResetSkill();
                timeCount = 0;
                isCounter = false;
            }
        }
    }

    void ResetSkill() {
        currOverCount = 0;
        if(modifySkill.TempSkillID == powerID) {
            modifySkill.ReplaceSkillCfg(originalID);
        }
        else if(modifySkill.TempSkillID == superPowerID) {
            modifySkill.ReplaceSkillCfg(1024);
        }
        else {
            this.Log("reset skill alread done.");
        }

    }
}
