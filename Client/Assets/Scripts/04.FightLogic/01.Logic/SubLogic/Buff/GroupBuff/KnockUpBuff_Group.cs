using System.Collections.Generic;
using cfg;

public class KnockUpBuff_Group : Buff {
    public KnockUpBuff_Group(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    protected override void Start() {
        base.Start();

        targetList = new List<MainLogicUnit>();
        targetList = CalcRule.FindMulipleTargetByRule(owner, cfg.impacter, skill.skillArgs);
        for(int i = 0; i < targetList.Count; i++) {
            targetList[i].KnockupCount += 1;
        }
    }

    protected override void End() {
        base.End();

        for(int i = 0; i < targetList.Count; i++) {
            targetList[i].KnockupCount -= 1;
        }
    }
}