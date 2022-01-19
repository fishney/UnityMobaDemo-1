using PEMath;

public class Soldier : MainLogicUnit
{
    public int soldierID;
    public int waveIndex;
    public int orderIndex;
    public string soldierName;

    /// <summary>
    /// 搜索距离的平方。避免开根号用的。
    /// </summary>
    PEInt sqrSearchDis;
    TargetCfg cfg;
    
    
    public Soldier(SoldierData sd) : base(sd)
    {
        soldierID = sd.soldierID;
        waveIndex = sd.waveIndex;
        orderIndex = sd.orderIndex;

        unitType = UnitTypeEnum.Soldier;
        unitName = sd.unitCfg.unitName + "_w:" + waveIndex + "_o:" + orderIndex;

        pathPrefix = "ResChars";
    }
    
    public override void LogicInit() {
        base.LogicInit();
        sqrSearchDis = (PEInt)skillArr[0].skillCfg.targetCfg.searchDis * (PEInt)skillArr[0].skillCfg.targetCfg.searchDis;
        cfg = skillArr[0].skillCfg.targetCfg;
        InputMoveForwardKey();
    }
    
    /// <summary>
    /// 每5个逻辑帧执行一次AI
    /// </summary>
    private int AITickInterval = 5;
    private int AITickIntervalCounter = 0;
    public override void LogicTick() {
        base.LogicTick();
        if(AITickIntervalCounter < AITickInterval) {
            AITickIntervalCounter += 1;
            return;
        }
        else {
            AITickIntervalCounter = 0;
        }

        if(CanReleaseSkill(ud.unitCfg.skillArr[0])) {
            MainLogicUnit lockTarget = CalcRule.FindSingleTargetByRule(this, cfg, PEVector3.zero);
            if(lockTarget != null) {
                skillArr[0].ReleaseSkill(PEVector3.zero);
            }
            else {
                lockTarget = CalcRule.FindMinDisEnemyTargetWithoutRange(this, cfg);
                if(lockTarget != null) {
                    PEVector3 offsetDir = lockTarget.LogicPos - LogicPos;
                    PEInt sqrDis = offsetDir.sqrMagnitude;
                    if(sqrDis < sqrSearchDis) {
                        InputFakeMoveKey(offsetDir.normalized);
                    }
                    else {
                        InputMoveForwardKey();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 向前移动。小兵在找不到目标时，保持向前移动
    /// </summary>
    void InputMoveForwardKey() {
        if(IsTeam(TeamEnum.Blue)) {
            InputFakeMoveKey(PEVector3.right);
        }
        else {
            InputFakeMoveKey(PEVector3.left);
        }
    }
}