using UnityEngine;
using System.Collections.Generic;

public partial class PlayWindow {
    public Transform mapIconRoot;
    public Transform mapHeroIconRoot;

    private Dictionary<MainLogicUnit, ItemMapIcon> itemDic;
    private Vector3 refPos = Vector3.zero;//场景中心位置默认为（0，0，0）

    void InitMiniMap() {
        itemDic = new Dictionary<MainLogicUnit, ItemMapIcon>();
    }

    void UnInitMiniMap() {
        for(int i = mapHeroIconRoot.childCount - 1; i >= 0; --i) {
            Destroy(mapHeroIconRoot.GetChild(i).gameObject);
        }
        for(int i = mapIconRoot.childCount - 1; i >= 0; --i) {
            if(mapIconRoot.GetChild(i).name != "bgRoad") {
                Destroy(mapIconRoot.GetChild(i).gameObject);
            }
        }
        if(itemDic != null) {
            itemDic.Clear();
        }
    }

    public void AddMiniIconItemInfo(MainLogicUnit unit) {
        if(itemDic.ContainsKey(unit)) {
            this.Error(unit.unitName + "minimap item is already exist.");
            return;
        }
        else {
            if(gameObject.activeSelf == false) {
                return;
            }
            string path = GetItemPath(unit.unitType);
            GameObject go = resSvc.LoadPrefab(path, true);
            if(unit.unitType == UnitTypeEnum.Hero) {
                go.transform.SetParent(mapHeroIconRoot);
            }
            else {
                go.transform.SetParent(mapIconRoot);
            }
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            ItemMapIcon ih = go.GetComponent<ItemMapIcon>();
            ih.InitItem(unit, refPos);
            itemDic.Add(unit, ih);
        }
    }

    string GetItemPath(UnitTypeEnum unitType) {
        string path = "";
        switch(unitType) {
            case UnitTypeEnum.Hero:
                path = "UIPrefab/DynamicItem/ItemMapIconHero";
                break;
            case UnitTypeEnum.Soldier:
            case UnitTypeEnum.Tower:
                path = "UIPrefab/DynamicItem/ItemMapIcon";
                break;
            default:
                break;
        }
        return path;
    }

    public void RemoveMapIconItemInfo(MainLogicUnit key) {
        if(itemDic.TryGetValue(key, out ItemMapIcon item)) {
            Destroy(item.gameObject);
            itemDic.Remove(key);
        }
    }
}