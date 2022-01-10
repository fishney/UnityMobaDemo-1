using PEMath;

public class TargetFlashMoveBuffCfg : BuffCfg {
    public float offset;//目标偏移量
}

/// <summary>
/// 目标闪现移动buff 亚瑟3技能
/// </summary>
public class TargetFlashMoveBuff : Buff {
    PEInt offset;

    public TargetFlashMoveBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();
        TargetFlashMoveBuffCfg tfmbc = cfg as TargetFlashMoveBuffCfg;
        offset = (PEInt)tfmbc.offset;
    }

    protected override void Start() {
        base.Start();

        MainLogicUnit target = CalcRule.FindSingleTargetByRule(owner, skill.skillCfg.targetCfg, PEVector3.zero);
        if(target == null) {
            // 预防目标死亡,报空
            unitState = SubUnitState.End;
        }
        else {
            PEVector3 disVec = target.LogicPos - owner.LogicPos;
            owner.LogicPos += disVec.normalized * (disVec.magnitude - offset);
        }
    }
}