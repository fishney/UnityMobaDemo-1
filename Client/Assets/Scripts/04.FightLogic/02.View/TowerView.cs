using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TowerView : MainViewUnit {
    Tower tower;

    public override void Init(LogicUnit logicUnit) {
        base.Init(logicUnit);

        tower = logicUnit as Tower;
    }

    public override void PlayAni(string aniName) { }

    public void DestroyTower() {
        Transform transMapRoot = GameObject.FindGameObjectWithTag("MapRoot").transform;
        MapRoot mapRoot = transMapRoot.GetComponent<MapRoot>();
        MainLogicUnit self = BattleSys.Instance.GetSelfHero();

        switch(tower.towerID) {
            case 1001:
                if(self.IsTeam(TeamEnum.Blue)) {
                    BattleSys.Instance.PlayBattleFieldAudio("selfTowerDestroy");
                }
                else {
                    BattleSys.Instance.PlayBattleFieldAudio("destroyEnemyTower");
                }
                mapRoot.DestroyBlueTower();
                break;
            case 1002:
                mapRoot.DestroyBlueCrystal();
                break;
            case 2001:
                if(self.IsTeam(TeamEnum.Blue)) {
                    BattleSys.Instance.PlayBattleFieldAudio("destroyEnemyTower");
                }
                else {
                    BattleSys.Instance.PlayBattleFieldAudio("selfTowerDestroy");
                }
                mapRoot.DestroyRedTower();
                break;
            case 2002:
                mapRoot.DestroyRedCrystal();
                break;
            default:
                break;
        }

        RemoveUIItemInfo();
        Destroy(gameObject, 1F);
    }
}