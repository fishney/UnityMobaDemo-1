using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PEMath;
using PEUtils;

/// <summary>
/// 战斗计算规则
/// </summary>
public static class CalcRule
{
    public static Hero[] blueTeamHero;
    public static Hero[] redTeamHero;
    public static Tower[] blueTeamTower;
    public static Tower[] redTeamTower;
    public static List<Soldier> blueTeamSoldier = new List<Soldier>();
    public static List<Soldier> redTeamSoldier = new List<Soldier>();

    /// <summary>
    /// 通过规则找到最近的单个目标
    /// </summary>
    /// <param name="self">自己逻辑单元</param>
    /// <param name="cfg">寻找目标配置</param>
    /// <param name="pos">TODO</param>
    /// <returns>找到的目标逻辑单元</returns>
    public static MainLogicUnit FindSingleTargetByRule(MainLogicUnit self, TargetCfg cfg, PEVector3 pos)
    {
        // 1.根据配置查找到所有活着的目标单位
        List<MainLogicUnit> targetList = GetTargetTeam(self, cfg);

        // 2.根据技能配置规则，确定单位
        switch (cfg.selectRule)
        {
            case SelectRuleEnum.MinHpValue:
                break;
            case SelectRuleEnum.MinHpPercent:
                break;
            case SelectRuleEnum.TargetClosestSingle:
                return FindMinDisTarget(self,targetList,(PEInt)cfg.selectRange);
            case SelectRuleEnum.PositionClosestSingle:
                break;
            default:
                PELog.Warn("selectRule unknown error");
        }
        
        
    }

    static MainLogicUnit FindMinDisTarget(MainLogicUnit self,List<MainLogicUnit> targetList, PEInt range)
    {
        if (targetList == null || targetList.Count < 1)
        {
            return null;
        }

        MainLogicUnit target = null;
        PEVector3 selfPos = self.LogicPos;
        PEInt len = 0;
        for (int i = 0; i < targetList.Count; i++)
        {
            PEInt sumRaius = targetList[i].ud.unitCfg.colliCfg.mRadius + self.ud.unitCfg.colliCfg.mRadius;
            // 要剔除掉半径，因为某单位半径可能因为体型变大而变得非常大，结果就看上去更近了，应该打这个看上去更近的
            PEInt tmpLen = (targetList[i].LogicPos - selfPos).magnitude - sumRaius;
            if (target == null || tmpLen < len)
            {
                len = tmpLen;
                target = targetList[i];
            }
        }

        return len < range ? target : null;
    }

    static List<MainLogicUnit> GetTargetTeam(MainLogicUnit self, TargetCfg cfg)
    {
        var targetList = new List<MainLogicUnit>();
        
        // 1.获取所有目标单位
        if (self.IsTeam(TeamEnum.Blue))
        {
            if (cfg.targetTeam == TargetTeamEnum.Friend)
            {
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Hero))
                {
                    targetList.AddRange(blueTeamHero);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Tower))
                {
                    targetList.AddRange(blueTeamTower);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Soldier))
                {
                    targetList.AddRange(blueTeamSoldier);
                }
            }
            else if (cfg.targetTeam == TargetTeamEnum.Enemy)
            {
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Hero))
                {
                    targetList.AddRange(redTeamHero);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Tower))
                {
                    targetList.AddRange(redTeamTower);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Soldier))
                {
                    targetList.AddRange(redTeamSoldier);
                }
            }
        }
        else  if (self.IsTeam(TeamEnum.Red))
        {
            if (cfg.targetTeam == TargetTeamEnum.Friend)
            {
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Hero))
                {
                    targetList.AddRange(redTeamHero);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Tower))
                {
                    targetList.AddRange(redTeamTower);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Soldier))
                {
                    targetList.AddRange(redTeamSoldier);
                }
            }
            else if (cfg.targetTeam == TargetTeamEnum.Enemy)
            {
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Hero))
                {
                    targetList.AddRange(blueTeamHero);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Tower))
                {
                    targetList.AddRange(blueTeamTower);
                }
                if (cfg.targetTypeArr.Any(o=> o == UnitTypeEnum.Soldier))
                {
                    targetList.AddRange(blueTeamSoldier);
                }
            }
        }
        else
        {
            PELog.Warn("self team unknown error");
        }
        
        // 2.过滤死亡单位
        for (int i = targetList.Count - 1; i >= 0; i--)
        {
            if (targetList[i].unitState == UnitStateEnum.Dead)
            {
                // 倒叙遍历，所以可以直接RemoveAt
                targetList.RemoveAt(i);
            }
        }

        return targetList;
    }
    
}