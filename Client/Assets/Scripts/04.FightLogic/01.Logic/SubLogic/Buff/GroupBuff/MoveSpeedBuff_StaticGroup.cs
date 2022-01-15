using System.Collections.Generic;
using PEMath;

public class MoveSpeedBuff_StaticGroup : Buff {
    PEInt speedOffset;

    public MoveSpeedBuff_StaticGroup(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        MoveSpeedBuffCfg msbc = cfg as MoveSpeedBuffCfg;
        speedOffset = msbc.amount;

        targetList = new List<MainLogicUnit>();

        switch(msbc.staticPosType) {
            case StaticPosTypeEnum.SkillCasterPos:
                LogicPos = source.LogicPos;
                break;
            case StaticPosTypeEnum.SkillLockTargetPos:
                LogicPos = skill.lockTarget.LogicPos;
                break;
            case StaticPosTypeEnum.BulletHitTargetPos:
                LogicPos = (PEVector3)args[1];
                break;
            case StaticPosTypeEnum.UIInputPos:
                LogicPos = source.LogicPos + skill.skillArgs;
                break;
            case StaticPosTypeEnum.None:
            default:
                this.Error("static buff pos error.");
                break;
        }
    }

    protected override void Start() {
        base.Start();

        targetList.AddRange(CalcRule.FindMulipleTargetByRule(source, cfg.impacter, LogicPos));
        ModifyGroupMoveSpeed(speedOffset, true);
    }

    // 注意！！！如果buff检测频率为0，Tick不会被LogicTick调用到
    protected override void Tick() {
        base.Tick();
        
        ModifyGroupMoveSpeed(-speedOffset);

        targetList.Clear();
        targetList.AddRange(CalcRule.FindMulipleTargetByRule(source, cfg.impacter, LogicPos));
        ModifyGroupMoveSpeed(speedOffset, false);
    }

    protected override void End() {
        base.End();
        ModifyGroupMoveSpeed(-speedOffset);
        targetList.Clear();
        targetList = null;
    }

    void ModifyGroupMoveSpeed(PEInt offset, bool showJump = false) {
        for(int i = 0; i < targetList.Count; i++) {
            PEInt value = targetList[i].moveSpeedBase * (offset / 100);
            targetList[i].ModifyMoveSpeed(value, this, showJump);
        }
    }
}
