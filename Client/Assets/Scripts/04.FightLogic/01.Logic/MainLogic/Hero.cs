

public class Hero : MainLogicUnit
{
    public int heroId;
    public int posIndex;
    public string userName;
    
    public Hero(HeroData hd) : base(hd)
    {
        heroId = hd.heroId;
        posIndex = hd.posIndex;
        userName = hd.userName;

        unitType = UnitTypeEnum.Hero;
        unitName = ud.unitCfg.unitName + "_" + userName;
        pathPrefix = "ResChars";
    }
    
    public override void LogicInit()
    {
      base.LogicInit();
      
    }

    public override void LogicTick()
    {
        base.LogicTick();
        
    }

    public override void LogicUnInit()
    {
        base.LogicUnInit();
        
    }
}