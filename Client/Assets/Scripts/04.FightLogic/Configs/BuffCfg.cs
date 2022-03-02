// using System;
// using XNode;
//
// [Serializable]
// public class BuffCfg
// {
//     #region 属性
//     [Node.InputAttribute(Node.ShowBackingValue.Always)]
//     public int buffId;
//     public string buffName;
//     
//     /// <summary>
//     /// buff类型，用来创建不同类型的buff
//     /// </summary>
//     public BuffTypeEnum buffType;
//     /// <summary>
//     /// buff附着目标
//     /// </summary>
//     public AttachTypeEnum attacher;
//     public StaticPosTypeEnum staticPosType;
//     
//     /// <summary>
//     /// buff作用目标，如果为null默认影响附着对象
//     /// </summary>
//     public TargetCfg impacter;
//
//     #endregion
//     
//     #region 效果相关
//
//     public int buffDelay;
//     /// <summary>
//     /// buff效果触发频率(比如持续1秒1次)，如果为0就只调用Start，LogicTick不会调用到Tick
//     /// </summary>
//     public int buffInterval;
//     /// <summary>
//     /// buff持续时间（不包含delay）0：生效1次，-1：永久生效
//     /// </summary>
//     public int buffDuration;
//
//     #endregion
//
//     
//
//
//     #region 配置
//
//     public string buffAudio;
//     public string buffEffect;
//     public string hitTickAudio;
//
//     #endregion
// }
//
// /// <summary>
// /// buff类型，用来创建不同类型的buff
// /// </summary>
// public enum BuffTypeEnum {
//     None,
//     HPCure,//治疗
//
//     ModifySkill,
//     MoveSpeed_Single,//单体加速buff
//     ArthurMark,//Arthur1技能的标记伤害Buff
//     Silense,//沉默
//     TargetFlashMove,
//     DirectionFlashMove,//TODO
//     ExecuteDamage,
//     Knockup_Group,//群体击飞
//
//     Stun_Single_DynamicTime,
//
//     //houyi专区buff
//     HouyiActiveSkillModify,//Houyi主动技能修改buff
//     Scatter,
//
//     HouyiPasvAttackSpeed,//Houyi被动攻速加成buff
//     HouyiPasvSkillModify,//Houyi被动技能修改Buff
//     HouyiPasvMultiArrow,//Houyi被动技能多重射击Buff
//     HouyiMixedMultiScatter,//混合多重射击与散射
//
//
//     MoveSpeed_DynamicGroup,//动态群体移速Buff
//     MoveSpeed_StaticGroup,//静态群体移速buff
//     Damage_DynamicGroup,//动态群体伤害
//     Damage_StaticGroup,
//     MoveAttack,//移动攻击
//     
//     
//     
// }
//
// /// <summary>
// /// buff位置确定方式
// /// </summary>
// public enum StaticPosTypeEnum {
//     None,
//     SkillCasterPos,//Buff所属技能施放者的位置
//     SkillLockTargetPos,//Buff所属技能锁定目标的位置
//     BulletHitTargetPos,//子弹命中目标的位置
//     UIInputPos,//UI输入位置信息，比如后裔2技能
// }
//
// /// <summary>
// /// buff附着目标
// /// </summary>
// public enum AttachTypeEnum {
//     None,
//     Caster,//由施术者自己确定：给自己，Arthur的1技能加速buff
//     Indie,//由施术者自己确定：区域，Arthur大招(位置固定)产生的持续范围伤害
//     
//     Target,//由受击者确定：给目标，Arthur的1技能沉默buff
//     Bullet,//由受击者确定：Houyi大招命中(位置动态)目标时产生的范围伤害
// }