using System.Collections.Generic;
using HOKProtocol;

public class Buff : SubLogicUnit
{
    /// buff附着单位
    protected MainLogicUnit owner;
    protected int buffId;
    protected object[] args;
    
    protected int buffDuration
    {
        get => cfg.buffDuration;
    }
    int tickCount = 0;//Dot计数
    int durationCount = 0;//时长计时
    public BuffCfg cfg;
    
    // 群体buff作用目标列表
    protected List<MainLogicUnit> targetLst;
    public Buff(MainLogicUnit source, Skill skill,MainLogicUnit owner,int buffId,object[] args = null) : base(source, skill)
    {
        this.owner = owner;
        this.buffId = buffId;
        this.args = args;
        
    }
    
    public override void LogicInit() {
        cfg = ResSvc.Instance().GetBuffConfigById(buffId);
        //buffDuration = cfg.buffDuration;
        base.delayTime = cfg.buffDelay;

        base.LogicInit();
    }

    public override void LogicTick() {
        base.LogicTick();
        switch(unitState) {
            case SubUnitState.Start:
                Start();
                // buffDuration: buff持续时间（不包含delay）0：生效1次，-1：永久生效
                if(buffDuration > 0 || buffDuration == -1) {
                    unitState = SubUnitState.Tick;
                }
                else {
                    unitState = SubUnitState.End;
                }
                break;
            case SubUnitState.Tick:
                if(cfg.buffInterval > 0) {
                    tickCount += Configs.ServerLogicFrameIntervelMs;
                    if(tickCount >= cfg.buffInterval) {
                        tickCount -= cfg.buffInterval;
                        Tick();
                    }
                }
                durationCount += Configs.ServerLogicFrameIntervelMs;
                if(durationCount >= buffDuration && buffDuration != -1) {
                    unitState = SubUnitState.End;
                }
                break;
        }
    }
    
    protected override void Start()
    {
        
    }

    protected override void Tick()
    {
        
    }

    protected override void End()
    {
        
    }
}