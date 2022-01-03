using System.Collections.Generic;
using PEMath;

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

        ModifyMoveSpeed(speedOffset, true);
    }

    protected override void Tick() {
        base.Tick();
        ModifyMoveSpeed(-speedOffset);

        targetList.Clear();
        targetList.AddRange(CalcRule.FindMulipleTargetByRule(owner, cfg.impacter, PEVector3.zero));
        ModifyMoveSpeed(speedOffset);
    }

    protected override void End() {
        base.End();
        ModifyMoveSpeed(-speedOffset);
        targetList.Clear();
        targetList = null;
    }

    void ModifyMoveSpeed(PEInt value, bool showJump = false) {
        for(int i = 0; i < targetList.Count; i++) {
            PEInt offset = targetList[i].moveSpeedBase * (value / 100);
            targetList[i].ModifyMoveSpeed(offset, this, showJump);
        }
    }
}