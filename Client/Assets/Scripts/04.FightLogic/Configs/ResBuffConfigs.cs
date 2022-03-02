//
//
// /// <summary>
// /// Buff数据,正常应该配表完成
// /// </summary>
// public class ResBuffConfigs
// {
//     #region 通用Buff
//     public static BuffCfg buff_90000 = new BuffCfg {
//         //通用buff属性
//         buffId = 90000,
//         buffName = "移动攻击",
//         buffType = BuffTypeEnum.MoveAttack,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 66,// 1帧检测1次
//         buffDuration = 5000,// 单次持续时间
//     };
//     #endregion
//
//     #region Arthur被动技能Buff
//     public static BuffCfg buff_10100 = new HPCureBuffCfg {
//         //通用buff属性
//         buffId = 10100,
//         buffName = "被动治疗",
//         buffType = BuffTypeEnum.HPCure,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 2000,
//         buffDuration = -1,
//
//         //专有属性
//         cureHPpct = 2,
//     };
//     #endregion
//
//     #region Arthur1技能Buff
//     public static BuffCfg buff_10110 = new MoveSpeedBuffCfg {
//         //通用buff属性
//         buffId = 10110,
//         buffName = "加速",
//         buffType = BuffTypeEnum.MoveSpeed_Single,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 3000,
//
//         //专有属性，提速30%
//         amount = 30,
//     };
//
//     public static BuffCfg buff_10111 = new CommonModifySkillBuffCfg {
//         //通用buff属性
//         buffId = 10111,
//         buffName = "替换普攻",
//         buffType = BuffTypeEnum.ModifySkill,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 3000,
//
//         //专有属性
//         originalID = 1010,
//         replaceID = 1014
//     };
//
//     /// <summary>
//     /// arthur1技能普攻修饰buff命中后的沉默buff
//     /// </summary>
//     public static BuffCfg buff_10140 = new BuffCfg {
//         //通用buff属性
//         buffId = 10140,
//         buffName = "沉默",
//         buffType = BuffTypeEnum.Silense,
//
//         attacher = AttachTypeEnum.Target,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 1000,
//     };
//
//     public static BuffCfg buff_10141 = new ArthurMarkBuffCfg {
//         //通用buff属性
//         buffId = 10141,
//         buffName = "Arthur1技能标记",
//         buffType = BuffTypeEnum.ArthurMark,
//
//         attacher = AttachTypeEnum.Target,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 5000,
//
//         damagePct = 1
//     };
//
//     public static BuffCfg buff_10142 = new MoveSpeedBuffCfg {
//         //通用buff属性
//         buffId = 10142,
//         buffName = "范围友军加速",
//         buffType = BuffTypeEnum.MoveSpeed_DynamicGroup,
//
//         attacher = AttachTypeEnum.Target,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,// 目标的敌人 = 友军
//             selectRule = SelectRuleEnum.TargetClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero
//             },
//             selectRange = 5f
//         },
//
//         buffDelay = 0,
//         buffInterval = 66,
//         buffDuration = 5000,
//
//         amount = 10
//     };
//     #endregion
//     
//     #region Arthur2技能Buff
//     public static BuffCfg buff_10120 = new DamageBuffCfg_DynamicGroup {
//         //通用buff属性
//         buffId = 10120,
//         buffName = "范围伤害",
//         buffType = BuffTypeEnum.Damage_DynamicGroup,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,// 目标的友军 = 敌人
//             selectRule = SelectRuleEnum.TargetClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 2f
//         },
//
//         buffDelay = 0,
//         buffInterval = 1000,
//         buffDuration = 5000,
//         staticPosType = StaticPosTypeEnum.None,
//
//         hitTickAudio = "com_hit1",
//         buffEffect = "Effect_sk2",
//
//         //专有参数
//         damage = 100
//     };
//     #endregion
//     
//     #region Arthur3技能Buff
//     public static BuffCfg buff_10130 = new TargetFlashMoveBuffCfg {
//         //通用buff属性
//         buffId = 10130,
//         buffName = "目标闪现跳跃",
//         buffType = BuffTypeEnum.TargetFlashMove,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//
//         //专有参数
//         offset = 1.5F
//     };
//     public static BuffCfg buff_10131 = new ExecuteDamageBuffCfg {
//         //通用buff属性
//         buffId = 10131,
//         buffName = "百分比生命伤害",
//         buffType = BuffTypeEnum.ExecuteDamage,
//
//         attacher = AttachTypeEnum.Target,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//
//         //专有参数
//         damagePct = 12
//     };
//
//     public static BuffCfg buff_10132 = new ExecuteDamageBuffCfg {
//         //通用buff属性
//         buffId = 10132,
//         buffName = "范围击飞",
//         buffType = BuffTypeEnum.Knockup_Group,
//
//         attacher = AttachTypeEnum.Target,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Friend,// 目标的友军 = 敌人
//             selectRule = SelectRuleEnum.TargetClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier
//             },
//             selectRange = 2f
//         },
//
//         buffDelay = 100,
//         buffInterval = 0,
//         buffDuration = 500,
//     };
//
//     public static BuffCfg buff_10133 = new DamageBuffCfg_StaticGroup {
//         //通用buff属性
//         buffId = 10133,
//         buffName = "固定位置范围伤害",
//         buffType = BuffTypeEnum.Damage_StaticGroup,
//
//         attacher = AttachTypeEnum.Indie,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.PositionClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier
//             },
//             selectRange = 2f
//         },
//
//         buffDelay = 100,
//         buffInterval = 1000,
//         buffDuration = 5000,
//         staticPosType = StaticPosTypeEnum.SkillLockTargetPos,
//
//         //effect
//         buffEffect = "Effect_sk3",
//         hitTickAudio = "com_hit1",
//
//         damage = 50
//     };
//      #endregion
//      
//     #region Houyi被动技能Buff
//      public static BuffCfg buff_10200 = new HouyiPasvAttackSpeedBuffCfg {
//          //通用buff属性
//          buffId = 10200,
//          buffName = "被动攻速加成叠加",
//          buffType = BuffTypeEnum.HouyiPasvAttackSpeed,
//
//          attacher = AttachTypeEnum.Caster,
//          impacter = null,
//
//          buffDelay = 0,
//          buffInterval = 66,
//          buffDuration = -1,
//
//          //专有属性
//          overCount = 3,
//          speedAddtion = 10,
//          resetTime = 3000,
//      };
//      public static BuffCfg buff_10201 = new HouyiMultipleSkillModifyBuffCfg {
//          //通用buff属性
//          buffId = 10201,
//          buffName = "被动普攻修改buff",
//          buffType = BuffTypeEnum.HouyiPasvSkillModify,
//
//          attacher = AttachTypeEnum.Caster,
//          impacter = null,
//
//          buffDelay = 0,
//          buffInterval = 66,
//          buffDuration = -1,
//
//          //专有属性
//          originalID = 1020,
//          powerID = 1025,
//          superPowerID = 1026,
//          triggerOverCount = 3,
//          resetTime = 3000,
//      };
//
//      //Houyi被动技能强化普攻为多重射击
//      public static BuffCfg buff_10250 = new HouyiMultipleArrowBuffCfg {
//          //通用buff属性
//          buffId = 10250,
//          buffName = "1技能强化普攻为多重射击",
//          buffType = BuffTypeEnum.HouyiPasvMultiArrow,
//      
//          attacher = AttachTypeEnum.Caster,
//          impacter = null,
//          buffDelay = 0,
//          buffInterval = 0,
//          buffDuration = 0,
//      
//          //专用属性
//          arrowCount = 2,
//          arrowDelay = 100,
//          posOffset = 0.3f,
//      };
//      #endregion
//      
//     #region Houyi1技能Buff
//     /// <summary>
//     /// Houyi1技能普攻替换buff
//     /// </summary>
//     public static BuffCfg buff_10210 = new HouyiScatterSkillModifyBuffCfg {
//         //通用buff属性
//         buffId = 10210,
//         buffName = "技能强化普攻",
//         buffType = BuffTypeEnum.HouyiActiveSkillModify,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 5000,//散射buff有效时间
//
//         //专有属性
//         originalID = 1020,
//         powerID = 1024,
//         superPowerID = 1026,
//     };
//     /// <summary>
//     /// Houyi1技能强化普攻后，散射buff
//     /// </summary>
//     public static BuffCfg buff_10240 = new HouyiScatterArrowBuffCfg {
//         //通用buff属性
//         buffId = 10240,
//         buffName = "1技能强化普攻为散射",
//         buffType = BuffTypeEnum.Scatter,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//
//         //专用属性
//         scatterCount = 2,
//         targetCfg = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.TargetClosestMulti,
//             //散射只能作用于小兵或英雄
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 5
//         },
//
//         damagePct = 50,
//     };
//     //Houyi1技能与被动混合强化普攻Buff:多重射击与散射混合
//     public static BuffCfg buff_10260 = new HouyiMixedMultiScatterBuffCfg {
//         //通用buff属性
//         buffId = 10260,
//         buffName = "1技能强化普攻为散射",
//         buffType = BuffTypeEnum.HouyiMixedMultiScatter,
//
//         attacher = AttachTypeEnum.Caster,
//         impacter = null,
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//
//         //专用属性
//         scatterCount = 2,
//         targetCfg = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.TargetClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 //散射普攻目标可以是所有，散射子弹的目标通过buff里的目标来配置
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 5
//         },
//         damagePct = 50,
//         arrowCount = 2,
//         arrowDelay = 50,
//         posOffset = 0.3f,
//     };
//     #endregion
//     
//     #region Houyi2技能Buff
//     /// <summary>
//     /// 后羿2技能动态范围伤害1，基于UI输入位置创建buff
//     /// </summary>
//     public static BuffCfg buff_10220 = new DamageBuffCfg_StaticGroup {
//         //通用buff属性
//         buffId = 10220,
//         buffName = "后羿2技能范围伤害1",
//         buffType = BuffTypeEnum.Damage_StaticGroup,
//
//         attacher = AttachTypeEnum.Indie,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.PositionClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 2f
//         },
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 2000,
//         staticPosType = StaticPosTypeEnum.UIInputPos,
//
//         damage = 200,
//         buffEffect = "houyi_sk2_effect",
//     };
//     /// <summary>
//     /// 中心位置的额外伤害
//     /// 后羿2技能额外动态范围伤害2，基于UI输入位置创建buff
//     /// </summary>
//     public static BuffCfg buff_10221 = new DamageBuffCfg_StaticGroup {
//         //通用buff属性
//         buffId = 10221,
//         buffName = "后羿2技能额外范围伤害2",
//         buffType = BuffTypeEnum.Damage_StaticGroup,
//
//         attacher = AttachTypeEnum.Indie,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.PositionClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 1f
//         },
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//         staticPosType = StaticPosTypeEnum.UIInputPos,
//
//         damage = 100,
//     };
//
//     /// <summary>
//     /// 后羿2技能静态范围移速1
//     /// </summary>
//     public static BuffCfg buff_10222 = new MoveSpeedBuffCfg {
//         //通用buff属性
//         buffId = 10222,
//         buffName = "后羿2技能动态范围移速1",
//         buffType = BuffTypeEnum.MoveSpeed_StaticGroup,
//         //施法时候释放的独立buff
//         attacher = AttachTypeEnum.Indie,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.PositionClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 2f
//         },
//         buffDelay = 0,
//         buffInterval = 0,// 注意！！！buff检测频率为0，所以只进行一次
//         buffDuration = 2000,//减速2秒
//         staticPosType = StaticPosTypeEnum.UIInputPos,
//
//         //专用参数
//         amount = -30,
//     };
//     /// <summary>
//     /// 后羿2技能额外静态范围移速2
//     /// </summary>
//     public static BuffCfg buff_10223 = new MoveSpeedBuffCfg {
//         //通用buff属性
//         buffId = 10223,
//         buffName = "后羿2技能额外动态范围移速2",
//         buffType = BuffTypeEnum.MoveSpeed_StaticGroup,
//         //施法时候释放的独立buff
//         attacher = AttachTypeEnum.Indie,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Enemy,
//             selectRule = SelectRuleEnum.PositionClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 1f
//         },
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 2000,
//         staticPosType = StaticPosTypeEnum.UIInputPos,
//
//         //专用参数
//         amount = -20,
//     };
//     #endregion
//     
//     #region Houyi3技能Buff
//     /// <summary>
//     /// Houyi3技能动态时间眩晕buff
//     /// </summary>
//     public static BuffCfg buff_10230 = new StunBuffCfg_DynamicTime {
//         //通用buff属性
//         buffId = 10230,
//         buffName = "动态时间眩晕",
//
//         attacher = AttachTypeEnum.Bullet,
//         impacter = null,
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//         buffType = BuffTypeEnum.Stun_Single_DynamicTime,
//
//         //专用参数
//         minStunTime = 1000,
//         maxStunTime = 3500,
//     };
//     /// <summary>
//     /// Houyi3技能范围伤害buff，基于子弹击中位置创建buff
//     /// </summary>
//     public static BuffCfg buff_10231 = new DamageBuffCfg_StaticGroup {
//         //通用buff属性
//         buffId = 10231,
//         buffName = "范围伤害",
//
//         attacher = AttachTypeEnum.Bullet,
//         impacter = new TargetCfg {
//             targetTeam = TargetTeamEnum.Friend,
//             selectRule = SelectRuleEnum.PositionClosestMulti,
//             targetTypeArr = new UnitTypeEnum[] {
//                 UnitTypeEnum.Hero,
//                 UnitTypeEnum.Soldier,
//             },
//             selectRange = 2f
//         },
//
//         buffDelay = 0,
//         buffInterval = 0,
//         buffDuration = 0,
//         buffType = BuffTypeEnum.Damage_StaticGroup,
//         staticPosType = StaticPosTypeEnum.BulletHitTargetPos,
//
//         //damageAudio = "com_hit1",
//         //effect
//         //buffEffect = "Effect_sk3",
//
//         damage = 100,
//     };
//     #endregion
// }