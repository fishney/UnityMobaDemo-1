using cfg;

/// <summary>
/// 技能替换buff
/// </summary>
public class CommonModifySkillBuff : Buff {
    public int originalID;
    public int replaceID;
    private Skill modifySkill;

    public CommonModifySkillBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source,  owner,skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        CommonModifySkillBuffCfg mabc = cfg as CommonModifySkillBuffCfg;
        originalID = mabc.originalID;
        replaceID = mabc.replaceID;
        modifySkill = owner.GetSkillByID(originalID);
    }

    protected override void Start() {
        base.Start();

        modifySkill.ReplaceSkillCfg(replaceID);
        modifySkill.SpellSuccessBp += ReplaceSkillReleaseDone;
    }

    void ReplaceSkillReleaseDone(Skill skillReleased) {
        // 如果上次成功释放得是一次普攻
        if(skillReleased.skillCfg.isNormalAttack) {
            unitState = SubUnitState.End;
        }
    }

    protected override void End() {
        base.End();
        modifySkill.ReplaceSkillCfg(originalID);
        modifySkill.SpellSuccessBp -= ReplaceSkillReleaseDone;
    }
}