using System;
using cfg;

/// <summary>
/// 技能替换buff
/// </summary>
public class CommonModifySkillBuff : Buff {
    public int originalID;
    public int replaceID;
    public int times;
    public int replaceIconId;
    public bool needBp;
    private Skill modifySkill;

    public CommonModifySkillBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source,  owner,skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        CommonModifySkillBuffCfg mabc = cfg as CommonModifySkillBuffCfg;
        originalID = mabc.originalID;
        replaceID = mabc.replaceID;
        times = mabc.times;
        needBp = times > 0;
        replaceIconId = mabc.replaceIconId;
        modifySkill = owner.GetSkillByID(originalID);
    }

    protected override void Start() {
        base.Start();

        modifySkill.ReplaceSkillCfg(replaceID,replaceIconId);
        
        if (needBp)
        {
            modifySkill.SpellSuccessBp += ReplaceSkillReleaseDone;
        }
        else
        {
            // 如果不需要BP，就直接销毁本buff
            unitState = SubUnitState.End;
        }
    }

    void ReplaceSkillReleaseDone(Skill skillReleased) {
        // 如果上次成功释放得是一次普攻
        if(skillReleased.skillCfg.isNormalAttack) {
            if (--times <= 0)
            {
                unitState = SubUnitState.End;
            }
        }
    }

    protected override void End() {
        base.End();
        if (needBp)
        {
            modifySkill.ReplaceSkillCfg(originalID);
            modifySkill.SpellSuccessBp -= ReplaceSkillReleaseDone; 
        }
    }
}