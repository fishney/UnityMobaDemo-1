

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

    bool setRevive;
    public override void LogicTick()
    {
        base.LogicTick();

        if (unitState == UnitStateEnum.Dead && setRevive == false)
        {
            setRevive = true;
            //更新击杀数据
            if(IsTeam(TeamEnum.Blue)) {
                BattleSys.Instance.SetKillData(TeamEnum.Red);
            }
            else {
                BattleSys.Instance.SetKillData(TeamEnum.Blue);
            }

            // TODO 死亡时间根据游戏时长变更
            var reviveTime = 5000;
            
            if(IsPlayerSelf()) {
                BattleSys.Instance.SetReviveState(true, reviveTime);
            }
            
            CreateLogicTimer(() => {
                setRevive = false;

                if(IsPlayerSelf()) {
                    BattleSys.Instance.SetReviveState(false);
                }
                unitState = UnitStateEnum.Alive;

                isDirChanged = true;
                LogicPos = ud.bornPos;
                mainViewUnit.ForcePosSync();
                ResetHP();
            }, reviveTime);
            
        }
    }

    public override void LogicUnInit()
    {
        base.LogicUnInit();
        
    }

    #region API Func

    public override bool IsPlayerSelf()
    {
        return posIndex == GameRoot.SelfPosIndex;
    }

    public override bool Equals(MainLogicUnit mainLogicUnit) {
        if(mainLogicUnit.unitType == unitType) {
            Hero hero = mainLogicUnit as Hero;
            return posIndex == hero.posIndex;
        }
        else {
            return false;
        }
    }
    #endregion
}