using PEMath;
using cfg;

/// <summary>
/// 百分比生命值斩杀
/// </summary>
public class ExecuteDamageBuff : Buff {
    PEInt damagePct;

    public ExecuteDamageBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        ExecuteDamageBuffCfg edbc = cfg as ExecuteDamageBuffCfg;
        damagePct = edbc.damagePct;
    }

    protected override void Start() {
        base.Start();

        PEInt damage = (damagePct / 100) * owner.ud.unitCfg.info.hp;
        owner.GetDamageByBuff(damage, this);
    }
}