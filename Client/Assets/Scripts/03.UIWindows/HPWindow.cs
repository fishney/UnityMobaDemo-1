/****************************************************
    文件：HPWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPWindow : WindowBase
{
    public Transform hpItemRoot;// 血条
    public Transform jumpNumRoot;// 伤害跳字
    public int jumpNumCount;// 缓存池数量

    private Dictionary<MainLogicUnit, ItemHP> itemDic;
    
    protected override void InitWindow()
    {
        base.InitWindow();
        itemDic = new Dictionary<MainLogicUnit, ItemHP>();
    }
    
    protected override void ClearWindow()
    {
        base.ClearWindow();
    }
    
    public void AddHPItemInfo(MainLogicUnit unit,Transform trans,int hp)
    {
        if (itemDic.ContainsKey(unit))
        {
            this.Error(unit.unitName + "hp item is already exist.");
        }
        else
        {
            // 判断单位类型
            string path = GetItemPath(unit.unitType);
            GameObject go = resSvc.LoadPrefab(path, true);
            go.transform.SetParent(hpItemRoot);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            ItemHP ih = go.GetComponent<ItemHP>();
            ih.InitItem(unit,trans,hp);
            itemDic.Add(unit,ih);
        }
    }

    string GetItemPath(UnitTypeEnum unitType) {
        string path = "";
        switch(unitType) {
            case UnitTypeEnum.Hero:
                path = "UIPrefab/DynamicItem/ItemHPHero";
                break;
            case UnitTypeEnum.Soldier:
                path = "UIPrefab/DynamicItem/ItemHPSoldier";
                break;
            case UnitTypeEnum.Tower:
                path = "UIPrefab/DynamicItem/ItemHPTower";
                break;
            default:
                break;
        }
        return path;
    }

    public void RemoveHPItemInfo(MainLogicUnit key)
    {
        if (itemDic.TryGetValue(key,out var item))
        {
            Destroy(item.gameObject);
            itemDic.Remove(key);
        }
    }

    public void SetHPVal(MainLogicUnit key, int hp)
    {
        if (itemDic.TryGetValue(key,out var item))
        {
            item.UpdateHPPrg(hp);
        }
    }
}
