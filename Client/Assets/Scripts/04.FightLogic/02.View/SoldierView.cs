using cfg;

public class SoldierView : MainViewUnit {
    Soldier soldier;
    public override void Init(LogicUnit logicUnit) {
        base.Init(logicUnit);
        soldier = logicUnit as Soldier;
    }

    protected override void Update() {
        base.Update();

        if(soldier.unitState == UnitStateEnum.Dead) {
            DestroySoldier();
            RemoveUIItemInfo();
        }
    }

    void DestroySoldier() {
        Destroy(gameObject, 3f);
    }
}