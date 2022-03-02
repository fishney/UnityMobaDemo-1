using PEMath;
using cfg;

public class HPCureBuff_Single : Buff
{
    public PEInt cureHPpct;
    
    public HPCureBuff_Single(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffId, object[] args = null) : base(source, owner,skill, buffId, args)
    {
    }
    
    public override void LogicInit() {
        base.LogicInit();

        HPCureBuffCfg hcbc = cfg as HPCureBuffCfg;
        cureHPpct = hcbc.cureHPpct;
    }
    
    protected override void Tick() {
        base.Tick();
        if(owner.unitState == UnitStateEnum.Alive) {
            owner.GetCureByBuff(owner.ud.unitCfg.info.hp * cureHPpct / 100, this);
        }
    }
}