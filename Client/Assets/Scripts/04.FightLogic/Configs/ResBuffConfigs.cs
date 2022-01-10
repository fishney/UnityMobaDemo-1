/// <summary>
/// Buff数据,正常应该配表完成
/// </summary>
public class ResBuffConfigs
{
    #region 通用Buff
    public static BuffCfg buff_90000 = new BuffCfg {
        //通用buff属性
        buffId = 90000,
        buffName = "移动攻击",
        buffType = BuffTypeEnum.MoveAttack,

        attacher = AttachTypeEnum.Caster,
        impacter = null,

        buffDelay = 0,
        buffInterval = 66,// 1帧检测1次
        buffDuration = 5000,// 单次持续时间
    };
    #endregion

    #region Arthur被动技能Buff
    public static BuffCfg buff_10100 = new HPCureBuffCfg {
        //通用buff属性
        buffId = 10100,
        buffName = "被动治疗",
        buffType = BuffTypeEnum.HPCure,

        attacher = AttachTypeEnum.Caster,
        impacter = null,

        buffDelay = 0,
        buffInterval = 2000,
        buffDuration = -1,

        //专有属性
        cureHPpct = 2,
    };
    #endregion

    #region Arthur1技能Buff
    public static BuffCfg buff_10110 = new MoveSpeedBuffCfg {
        //通用buff属性
        buffId = 10110,
        buffName = "加速",
        buffType = BuffTypeEnum.MoveSpeed_Single,

        attacher = AttachTypeEnum.Caster,
        impacter = null,

        buffDelay = 0,
        buffInterval = 0,
        buffDuration = 3000,

        //专有属性，提速30%
        amount = 30,
    };

    public static BuffCfg buff_10111 = new CommonModifySkillBuffCfg {
        //通用buff属性
        buffId = 10111,
        buffName = "替换普攻",
        buffType = BuffTypeEnum.ModifySkill,

        attacher = AttachTypeEnum.Caster,
        impacter = null,

        buffDelay = 0,
        buffInterval = 0,
        buffDuration = 3000,

        //专有属性
        originalID = 1010,
        replaceID = 1014
    };

    /// <summary>
    /// arthur1技能普攻修饰buff命中后的沉默buff
    /// </summary>
    public static BuffCfg buff_10140 = new BuffCfg {
        //通用buff属性
        buffId = 10140,
        buffName = "沉默",
        buffType = BuffTypeEnum.Silense,

        attacher = AttachTypeEnum.Target,
        impacter = null,

        buffDelay = 0,
        buffInterval = 0,
        buffDuration = 1000,
    };

    public static BuffCfg buff_10141 = new ArthurMarkBuffCfg {
        //通用buff属性
        buffId = 10141,
        buffName = "Arthur1技能标记",
        buffType = BuffTypeEnum.ArthurMark,

        attacher = AttachTypeEnum.Target,
        impacter = null,

        buffDelay = 0,
        buffInterval = 0,
        buffDuration = 5000,

        damagePct = 1
    };

    public static BuffCfg buff_10142 = new MoveSpeedBuffCfg {
        //通用buff属性
        buffId = 10142,
        buffName = "范围友军加速",
        buffType = BuffTypeEnum.MoveSpeed_DynamicGroup,

        attacher = AttachTypeEnum.Target,
        impacter = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,// 目标的敌人 = 友军
            selectRule = SelectRuleEnum.TargetClosestMulti,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero
            },
            selectRange = 5f
        },

        buffDelay = 0,
        buffInterval = 66,
        buffDuration = 5000,

        amount = 10
    };
    #endregion
    
    #region Arthur2技能Buff
    public static BuffCfg buff_10120 = new DamageBuffCfg_DynamicGroup {
        //通用buff属性
        buffId = 10120,
        buffName = "范围伤害",
        buffType = BuffTypeEnum.Damage_DynamicGroup,

        attacher = AttachTypeEnum.Caster,
        impacter = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,// 目标的友军 = 敌人
            selectRule = SelectRuleEnum.TargetClosestMulti,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier,
            },
            selectRange = 2f
        },

        buffDelay = 0,
        buffInterval = 1000,
        buffDuration = 5000,
        staticPosType = StaticPosTypeEnum.None,

        hitTickAudio = "com_hit1",
        buffEffect = "Effect_sk2",

        //专有参数
        damage = 100
    };
    #endregion
    
    #region Arthur3技能Buff
    public static BuffCfg buff_10130 = new TargetFlashMoveBuffCfg {
        //通用buff属性
        buffId = 10130,
        buffName = "目标闪现跳跃",
        buffType = BuffTypeEnum.TargetFlashMove,

        attacher = AttachTypeEnum.Caster,
        impacter = null,

        buffDelay = 0,
        buffInterval = 0,
        buffDuration = 0,

        //专有参数
        offset = 1.5F
    };
    public static BuffCfg buff_10131 = new ExecuteDamageBuffCfg {
        //通用buff属性
        buffId = 10131,
        buffName = "百分比生命伤害",
        buffType = BuffTypeEnum.ExecuteDamage,

        attacher = AttachTypeEnum.Target,
        impacter = null,

        buffDelay = 0,
        buffInterval = 0,
        buffDuration = 0,

        //专有参数
        damagePct = 12
    };

    public static BuffCfg buff_10132 = new ExecuteDamageBuffCfg {
        //通用buff属性
        buffId = 10132,
        buffName = "范围击飞",
        buffType = BuffTypeEnum.Knockup_Group,

        attacher = AttachTypeEnum.Target,
        impacter = new TargetCfg {
            targetTeam = TargetTeamEnum.Friend,// 目标的友军 = 敌人
            selectRule = SelectRuleEnum.TargetClosestMulti,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier
            },
            selectRange = 2f
        },

        buffDelay = 100,
        buffInterval = 0,
        buffDuration = 500,
    };

    public static BuffCfg buff_10133 = new DamageBuffCfg_StaticGroup {
        //通用buff属性
        buffId = 10133,
        buffName = "固定位置范围伤害",
        buffType = BuffTypeEnum.Damage_StaticGroup,

        attacher = AttachTypeEnum.Indie,
        impacter = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.PositionClosestMulti,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier
            },
            selectRange = 2f
        },

        buffDelay = 100,
        buffInterval = 1000,
        buffDuration = 5000,
        staticPosType = StaticPosTypeEnum.SkillLockTargetPos,

        //effect
        buffEffect = "Effect_sk3",
        hitTickAudio = "com_hit1",

        damage = 50
    };
     #endregion
}