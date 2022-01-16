public class Tower : MainLogicUnit
{
    public int towerID;
    public int towerIndex;
    
    public Tower(TowerData ud) : base(ud)
    {
        towerID = ud.towerID;
        towerIndex = ud.towerIndex;

        unitType = UnitTypeEnum.Tower;
        pathPrefix = "ResTower";
    }
    
    public override void LogicTick() {
        base.LogicTick();

        TickAI();
    }

    
    
}