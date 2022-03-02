using System.Collections.Generic;
using PEMath;
using cfg;

public class MoveSpeedBuff_DynamicGroup: Buff 
{
    PEInt speedOffset;
    
    public MoveSpeedBuff_DynamicGroup(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffId, object[] args = null) : base(source, owner, skill, buffId, args)
    {
    }
    
    public override void LogicInit() {
        base.LogicInit();
        targetList = new List<MainLogicUnit>();
        targetList.AddRange(CalcRule.FindMulipleTargetByRule(owner, cfg.impacter, skill.skillArgs));
        MoveSpeedBuffCfg msbc = cfg as MoveSpeedBuffCfg;
        speedOffset = msbc.amount;
    }

    protected override void Start() {
        base.Start();

        ModifyTargetsMoveSpeed(speedOffset, true);
    }

    protected override void Tick() {
        base.Tick();
        ModifyTargetsMoveSpeed(-speedOffset);

        targetList.Clear();
        targetList.AddRange(CalcRule.FindMulipleTargetByRule(owner, cfg.impacter, PEVector3.zero));
        ModifyTargetsMoveSpeed(speedOffset);
    }

    protected override void End() {
        base.End();
        ModifyTargetsMoveSpeed(-speedOffset);
        targetList.Clear();
        targetList = null;
    }

    void ModifyTargetsMoveSpeed(PEInt value, bool showJump = false) {
        for(int i = 0; i < targetList.Count; i++) {
            PEInt offset = targetList[i].moveSpeedBase * (value / 100);
            targetList[i].ModifyMoveSpeed(offset, this, showJump);
        }
    }
}