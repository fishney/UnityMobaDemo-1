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
    private string buffOnAudio;

    public NSSpeedPasvBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner,skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        NSSpeedBuffCfg buffCfg = cfg as NSSpeedBuffCfg;
        resetTime = buffCfg.resetTime;
        moveSpeedPer = buffCfg.moveSpeedPer;
        atkSpeedPer = buffCfg.atkSpeedPer;
        buffOnAudio = buffCfg.audio_buffOn;

        source.OnKillPlayer += OnKill;
    }
    
    void OnKill(MainLogicUnit killedUnit)
    {
        source.PlayAudio(buffOnAudio);
        SetOrExtendSpeed();
    }
    
    protected override void Start()
    {
        base.Start();
        // TODO 偷懒了，应该放在buff里设立字段
        owner.mainViewUnit.SetImgInfoIcon("jinx_pasv");
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
        // 不可叠加的buff。如果正在buff状态中，只会刷新时长，而不会叠加速度。
        owner.mainViewUnit.SetImgInfo(resetTime.RawInt);
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