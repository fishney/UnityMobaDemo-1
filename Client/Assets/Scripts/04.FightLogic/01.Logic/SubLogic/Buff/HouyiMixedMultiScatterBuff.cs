using PEMath;
using System.Collections.Generic;
using cfg;

public class HouyiMixedMultiScatterBuff : Buff {
    int scatterCount;//散射个数
    TargetCfg targetCfg;//散射目标查找规则
    int damagePct;//散射子弹伤害百分比
    MainLogicUnit lockTarget;

    int arrowCount;
    int arrowDelay;
    PEInt posOffset;

    public HouyiMixedMultiScatterBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        HouyiMixedMultiScatterBuffCfg hmmsbc = cfg as HouyiMixedMultiScatterBuffCfg;
        scatterCount = hmmsbc.scatterCount;
        targetCfg = hmmsbc.targetCfg;
        damagePct = hmmsbc.damagePct;

        targetList = new List<MainLogicUnit>();

        arrowCount = hmmsbc.arrowCount;
        arrowDelay = hmmsbc.arrowDelay;
        posOffset = (PEInt)hmmsbc.posOffset;

        lockTarget = skill.lockTarget;

        // 主箭3连射
        CreateMultiArrow(lockTarget, skill.skillCfg.damage, false);

        // 技能1散射3个目标 => 寻找额外的2个目标
        var findLst = CalcRule.FindMulipleTargetByRule(owner, targetCfg, PEVector3.zero);
        int count = 0;
        for(int i = 0; i < findLst.Count; i++) {
            if(count < scatterCount) {
                if(findLst[i].Equals(lockTarget)) {
                    continue;
                }
                else {
                    targetList.Add(findLst[i]);
                    count += 1;
                }
            }
        }
        this.Log("get!!!");
        for(int i = 0; i < targetList.Count; i++) {
            TargetBullet bullet = source.CreateSkillBullet(source, targetList[i], skill) as TargetBullet;
            bullet.HitTargetCB = (MainLogicUnit target, object[] args) => {
                // this.Log("scatter target name:" + target.unitName);
                target.GetDamageByBuff(skill.skillCfg.damage * damagePct / 100, this);
            };

            CreateMultiArrow(targetList[i], skill.skillCfg.damage * damagePct / 100, true);
        }
    }

    void CreateMultiArrow(MainLogicUnit target, PEInt damage, bool isCurve = false) {
        for(int i = 0; i < arrowCount; i++) {
            TargetBullet bullet = source.CreateSkillBullet(source, target, skill) as TargetBullet;
            if(isCurve) {
                bullet.SetCurveDir();
            }
            bullet.SetDelayData((i + 1) * arrowDelay);

            if(i % 2 == 0) {
                bullet.SetOffsetPos(PEVector3.up * posOffset);
            }
            else {
                bullet.SetOffsetPos(PEVector3.up * -posOffset);
            }

            bullet.HitTargetCB = (MainLogicUnit hitTarget, object[] args) => {
                hitTarget.GetDamageByBuff(damage, this);
            };
        }
    }
}
