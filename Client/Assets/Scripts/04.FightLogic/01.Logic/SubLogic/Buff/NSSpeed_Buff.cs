using PEMath;
using cfg;
using proto.HOKProtocol;
using UnityEngine;

/// <summary>
/// 不可叠加只能累计的加速类buff
/// </summary>
public class NSSpeedPasvBuff : Buff {
    private PEInt resetTime;
    private PEInt moveSpeedPer;
    private PEInt atkSpeedPer;

    public NSSpeedPasvBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner,skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        NSSpeedBuffCfg buffCfg = cfg as NSSpeedBuffCfg;
        resetTime = buffCfg.resetTime;
        moveSpeedPer = buffCfg.moveSpeedPer;
        atkSpeedPer = buffCfg.atkSpeedPer;

        source.OnKillPlayer += OnKill;
    }
    
    void OnKill(MainLogicUnit killedUnit)
    {
        SetOrExtendSpeed();
    }

    /// <summary>
    /// 用来计算是否到3秒
    /// </summary>
    private int timeCount;
    /// <summary>
    /// 是否在Tick计算是否到3秒
    /// </summary>
    private bool isInBuff;
    protected override void Tick() {
        base.Tick();
        if(isInBuff) {
            timeCount += Configs.ServerLogicFrameIntervelMs;
            if(timeCount >= resetTime) {
                ResetSpeed();
                timeCount = 0;
                isInBuff = false;
            }
        }
    }
    
    void SetOrExtendSpeed()
    {
        PEInt moveSpeedOffset = source.moveSpeedBase * (moveSpeedPer / 100);
        PEInt atkSpeedOffset = source.AttackSpeedRateBase * (atkSpeedPer / 100);
        Debug.Log("moveSpeedOffset " + moveSpeedOffset + ",atkSpeedOffset" + atkSpeedOffset + ",isInBuff" + isInBuff+ ",timeCount" + timeCount+ ",resetTime" + resetTime);
        // 不可叠加的buff。如果正在buff状态中，只会刷新时长，而不会叠加速度。
        if (isInBuff)
        {
            timeCount = 0;
        }
        else
        {
            isInBuff = true;
            SetSpeed();
        }
    }
    
    void ResetSpeed() {
        PEInt moveSpeedOffset = source.moveSpeedBase * (moveSpeedPer / 100);
        PEInt atkSpeedOffset = source.AttackSpeedRateBase * (atkSpeedPer / 100);
        owner.ModifyAttackSpeed(-atkSpeedOffset);
        owner.ModifyMoveSpeed(-moveSpeedOffset,this,false);
    }
    void SetSpeed() {
        PEInt moveSpeedOffset = source.moveSpeedBase * (moveSpeedPer / 100);
        PEInt atkSpeedOffset = source.AttackSpeedRateBase * (atkSpeedPer / 100);
        owner.ModifyAttackSpeed(atkSpeedOffset);
        owner.ModifyMoveSpeed(moveSpeedOffset,this,true); 
    }
}