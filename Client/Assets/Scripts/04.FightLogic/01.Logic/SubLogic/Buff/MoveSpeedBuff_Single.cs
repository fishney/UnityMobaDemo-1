using PEMath;
using cfg;

public class MoveSpeedBuff_Single : Buff {
    private PEInt speedOffset;

    public MoveSpeedBuff_Single(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner,skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        MoveSpeedBuffCfg msbc = cfg as MoveSpeedBuffCfg;
        speedOffset = owner.moveSpeedBase * ((PEInt)msbc.amount / 100);
    }

    protected override void Start() {
        base.Start();
        owner.ModifyMoveSpeed(speedOffset, this, true);
    }

    protected override void End() {
        base.End();
        owner.ModifyMoveSpeed(-speedOffset, this, false);
    }
}