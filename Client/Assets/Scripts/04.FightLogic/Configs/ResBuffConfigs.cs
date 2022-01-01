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
        buffInterval = 66,
        buffDuration = 5000
    };
    #endregion

    #region Arthur被动技能Buff
    public static BuffCfg buff_10100 = new HPCureBuffCfg {
        //通用buff属性
        buffID = 10100,
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
        buffID = 10111,
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
        buffID = 10140,
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
        buffID = 10141,
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
        buffID = 10142,
        buffName = "范围友军加速",
        buffType = BuffTypeEnum.MoveSpeed_DynamicGroup,

        attacher = AttachTypeEnum.Target,
        impacter = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosetMultiple,
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
}