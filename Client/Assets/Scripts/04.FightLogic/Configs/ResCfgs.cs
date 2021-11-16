using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCfg
{
   public int unitId;
   public string unitName;//单位角色名
   public string resName;//资源
}


public class MapCfg
{
   public int mapId;
   
   // 小兵出生间隔
   public int bornDelay;
   public int bornInterval;
   public int waveInterval;
}