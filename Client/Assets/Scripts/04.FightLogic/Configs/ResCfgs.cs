using System.Collections;
using System.Collections.Generic;
using PEMath;
using PEPhysx;
using UnityEngine;

#region 人物

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

public class UnitCfg
{
   public int unitId;
   public string unitName;//单位角色名
   public string resName;//资源

   // 核心属性
   public int hp;
   public int def;
   public int moveSpeed;
   
   // 碰撞体
   public ColliderConfig colliCfg;
   
   // 技能Id数组
   public int[] skillArr;
}

#endregion

#region 地图

public class MapCfg
{
   public int mapId;
   public PEVector3 blueBorn;
   public PEVector3 redBorn;
   
   // 小兵出生间隔
   public int bornDelay;
   public int bornInterval;
   public int waveInterval;
}

#endregion