using PEMath;
using cfg;

/// <summary>
/// 移动攻击buff
/// </summary>
public class MoveAttackBuff: Buff
{
    /// <summary>
    /// 目标单位
    /// </summary>
    MainLogicUnit moveTarget;
    SkillCfg atkSkillCfg;
    /// <summary>
    /// 技能范围
    /// </summary>
    PEInt selectRange;
    /// <summary>
    /// 搜索目标最远距离
    /// </summary>
    PEInt searchDis;

    bool activeSkill;
    
    public MoveAttackBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffId, object[] args = null) : base(source, owner, skill, buffId, args)
    {
    }
    
    public override void LogicInit() {
        base.LogicInit();
        atkSkillCfg = ResSvc.Instance().GetSkillConfigById(skill.skillId);
        
        selectRange = (PEInt)atkSkillCfg.targetCfg.selectRange;
        searchDis = (PEInt)atkSkillCfg.targetCfg.searchDis;
        activeSkill = false;
    }
    
    protected override void Start() {
        base.Start();
        MoveToTarget();
    }

    protected override void Tick() {
        base.Tick();
        MoveToTarget();
    }
    
    void MoveToTarget() {
        moveTarget = CalcRule.FindMinDisEnemyTargetWithoutRange(owner, skill.skillCfg.targetCfg);
        if(moveTarget == null) {
            return;
        }
        else {
            PEVector3 offsetDir = moveTarget.LogicPos - owner.LogicPos;
            PEInt sqrDis = offsetDir.sqrMagnitude;
            PEInt sumRadius = owner.ud.unitCfg.colliCfg.mRadius + moveTarget.ud.unitCfg.colliCfg.mRadius;
            if(sqrDis < (selectRange + sumRadius) * (selectRange + sumRadius)) {
                // 技能范围内查找到释放目标，释放技能
                activeSkill = true;
                BattleSys.Instance.SendMoveKey(PEVector3.zero);
                unitState = SubUnitState.End;
            }
            else {
                if(sqrDis < (searchDis + sumRadius) * (searchDis + sumRadius)) {
                    if(BattleSys.Instance.CheckUIInput()) {
                        //有UI输入,中断移动攻击
                        unitState = SubUnitState.End;
                    }
                    else {
                        // 搜索范围内查找到释放目标，往其移动
                        BattleSys.Instance.SendMoveKey(offsetDir.normalized);
                    }
                }
                else {
                    this.Log("超出搜索距离");
                    BattleSys.Instance.SendMoveKey(PEVector3.zero);
                    unitState = SubUnitState.End;
                }
            }
        }
    }
    
    protected override void End() {
        base.End();
        if(activeSkill) {
            activeSkill = false;
            BattleSys.Instance.SendSkillKey(skill.skillId);
        }
    }
}