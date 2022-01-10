

/// <summary>
/// 目标配置
/// </summary>
public class TargetCfg
{
    public TargetTeamEnum targetTeam;
    public SelectRuleEnum selectRule;
    /// 可以是多类目标：英雄、小兵、塔
    public UnitTypeEnum[] targetTypeArr;

    #region 辅助参数
    
    /// 查找目标范围距离
    public float selectRange;
    /// 移动攻击搜索距离 m
    public float searchDis;


    #endregion
}

/// <summary>
/// 目标选择规则
/// </summary>
public enum SelectRuleEnum
{
    None,
    
    // 单个目标选择规则
    
    MinHpValue,// 血量最小
    MinHpPercent,// 血量百分比最小
    TargetClosestSingle,// 最近的单个
    PositionClosestSingle,// 靠近某个位置的单个选择
    
    // 多个目标选择规则
    
    TargetClosestMulti,// 最近的多个（范围选择）
    PositionClosestMulti,// 靠近某个位置的多个选择（范围选择）
    
    Hero,// 所有英雄单位
}

/// 施法目标
public enum TargetTeamEnum
{
    /// 用于动态选择目标，通常是方向指向或位置指向技能，在施法成功后通过buff选择目标
    Dynamic,
    Friend,
    Enemy
}