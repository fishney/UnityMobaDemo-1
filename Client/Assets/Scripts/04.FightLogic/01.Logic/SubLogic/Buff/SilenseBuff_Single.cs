/// <summary>
/// 单体沉默buff：亚瑟技能1附带的buff
/// </summary>
public class SilenseBuff_Single: Buff {
    public SilenseBuff_Single(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    protected override void Start() {
        base.Start();

        owner.SilenceCount += 1;
    }

    protected override void End() {
        base.End();
        owner.SilenceCount -= 1;
    }
}
