using System.Collections.Generic;
using HOKProtocol;
using UnityEngine;

public class Buff : SubLogicUnit
{
    /// buff附着单位
    public MainLogicUnit owner;
    protected int buffId;
    /// <summary>
    /// 值在DirectionBullet类里有输入，通过调用 hitTargetCB 来获得的
    /// </summary>
    protected object[] args;
    // buff表现
    private BuffView buffView;
    
    protected int buffDuration
    {
        get => cfg.buffDuration;
    }
    int tickCount = 0;//Dot计数
    int durationCount = 0;//时长计时
    public BuffCfg cfg;
    
    // 群体buff作用目标列表
    protected List<MainLogicUnit> targetList;
    public Buff(MainLogicUnit source,MainLogicUnit owner, Skill skill, int buffId,object[] args = null) : base(source, skill)
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
                // 频率触发型buff需要按照频率来Tick，比如点燃
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
        //根据staticPosType决定Buff初始位置
        if(cfg.staticPosType == StaticPosTypeEnum.None) {
            LogicPos = owner.LogicPos;
        }
        
        if (cfg.buffEffect != null)
        {
            // 只是用资源，在服务端跑可以进行条件编译，这一段不需要
            GameObject go = ResSvc.Instance().LoadPrefab("ResImages/ResEffects/" + cfg.buffEffect);
            go.name = source.unitName + "_" + cfg.buffName;
            buffView = go.GetComponent<BuffView>();
            if (buffView == null)
            {
                this.Error("Get BuffView Error:" + unitName);
            }
            // 设定位置跟随
            if(cfg.staticPosType == StaticPosTypeEnum.None) {
                owner.mainViewUnit.SetBuffFollower(buffView);
            }
            buffView.Init(this);

            if(cfg.buffAudio != null) {
                buffView.PlayAudio(cfg.buffAudio);
            }
        }
        else {
            // 附着性buff
            if(cfg.buffAudio != null) {
                owner.PlayAudio(cfg.buffAudio);
            }
        }
    }

    protected override void Tick()
    {
        if (cfg.hitTickAudio != null && targetList?.Count > 0)
        {
            owner.PlayAudio(cfg.hitTickAudio);
        }
    }

    protected override void End()
    {
        if (cfg.buffEffect != null)
        {
            buffView.DestroyBuff();
        }
    }
}