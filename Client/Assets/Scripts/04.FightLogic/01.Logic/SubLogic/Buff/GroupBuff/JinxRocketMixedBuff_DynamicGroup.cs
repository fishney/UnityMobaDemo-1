using System.Collections.Generic;
using PEMath;
using cfg;
using UnityEngine;

public class JinxRocketMixedBuff_DynamicGroup : Buff
{
    public PEInt damage;
    public PEInt perDamagedHit;
    public PEInt perSplash;
    public JinxRocketMixedBuff_DynamicGroup(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();
        targetList = new List<MainLogicUnit>();
        JinxRocketMixedBuffCfg_DynamicGroup buffCfg = cfg as JinxRocketMixedBuffCfg_DynamicGroup;
        perDamagedHit = (PEInt)((float)buffCfg.perDamagedHit / 100);
        perSplash = (PEInt)((float)buffCfg.perSplash / 100);
        int argsTime = (int)args[0];// 约定index为0
        LogicPos = (PEVector3)args[1];// 约定index为1
        Debug.Log(argsTime);
        if(argsTime < buffCfg.minTime) {
            damage = buffCfg.minDamage;
        }
        else if(argsTime > buffCfg.maxTime) {
            damage = buffCfg.maxDamage;
        }
        else
        {
            damage = (int)(buffCfg.minDamage + (float)((buffCfg.maxDamage - buffCfg.minDamage)) / (buffCfg.maxTime - buffCfg.minTime));
        }
    }

    protected override void Start() {
        base.Start();
        CalcGroupDamage();
    }

    protected override void Tick() {
        base.Tick();
        CalcGroupDamage();
    }

    void CalcGroupDamage() {
        targetList.Clear();
        targetList.AddRange(CalcRule.FindMulipleTargetByRule(owner, cfg.impacter, LogicPos));
        for(int i = 0; i < targetList.Count; i++)
        {
            PEInt resultDamage;
            // 确定是命中单位还是溅射单位
            if (owner == targetList[i])
            {
                resultDamage = GetResultDamage(targetList[i], damage, perDamagedHit, 1);
            }
            else
            {
                resultDamage = GetResultDamage(targetList[i], damage, perDamagedHit, perSplash);
            }
            
            targetList[i].GetDamageByBuff(source, resultDamage, this);
        }
    }

    PEInt GetResultDamage(MainLogicUnit target, PEInt damage, PEInt perDamagedHit ,PEInt perSplash)
    {
        return (damage + (target.ud.unitCfg.info.hp - target.Hp) * perDamagedHit) * perSplash;
    }
}
