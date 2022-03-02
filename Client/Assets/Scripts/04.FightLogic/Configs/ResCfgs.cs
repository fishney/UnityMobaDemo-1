using System.Collections;
using System.Collections.Generic;
using Bright.Serialization;
using PEMath;
using PEPhysx;
using UnityEngine;
using cfg;

#region 人物

public class SoldierData : LogicUnitData {
   public int soldierID;
   public int waveIndex;
   public int orderIndex;
   public string soldierName;
}

public class TowerData : LogicUnitData {
   public int towerID;
   public int towerIndex;
   public string towerName;
}

public class HeroData : LogicUnitData
{
   public int heroId;
   public int posIndex;
   public string userName;
   
}

public class LogicUnitData
{
   public TeamEnum teamEnum;
   public PEVector3 bornPos;
   public UnitCfg unitCfg;
}

// TODO UnitInfoCfg => UnitInfoCfg内部类
public class UnitCfg
{
   // public int unitId;
   // public string unitName;//单位角色名
   // public string resName;//资源
   public PEInt hitHeight;// 被子弹命中的高度
   
   // // 核心属性
   // public int hp;
   // public int def;
   // public int moveSpeed;
   
   // 碰撞体
   public ColliderConfig colliCfg;
   
   // // 技能Id数组
   // /// <summary>
   // /// 被动技能buff
   // /// </summary>
   // public int[] pasvBuff;
   // public int[] skillArr;
   public UnitInfoCfg info;
}

// public class UnitInfoCfg
// {
//    public int unitId;
//    public string unitName;//单位角色名
//    public string resName;//资源
//    // public PEInt hitHeight;// 被子弹命中的高度
//    
//    // 核心属性
//    public int hp;
//    public int def;
//    public int moveSpeed;
//
//    public UnitTypeEnum colliderType;
//    
//    // 碰撞体
//    // public ColliderConfig colliCfg;
//    
//    // 技能Id数组
//    /// <summary>
//    /// 被动技能buff
//    /// </summary>
//    public int[] pasvBuff;
//    public int[] skillArr;
// }


#endregion

#region 地图

public class MapCfg
{
   public int mapId;
   public PEVector3 blueBorn;
   public PEVector3 redBorn;
   
   public int[] towerIDArr;
   public PEVector3[] towerPosArr;
   
   // 小兵出生间隔
   public int bornDelay;
   public int bornInterval;
   public int waveInterval;
   public int[] blueSoldierIDArr;
   public PEVector3[] blueSoldierPosArr;
   public int[] redSoldierIDArr;
   public PEVector3[] redSoldierPosArr;
}

#endregion