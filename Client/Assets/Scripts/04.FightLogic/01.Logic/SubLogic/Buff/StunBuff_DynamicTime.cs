using cfg;

public class StunBuff_DynamicTime : Buff {
    public StunBuff_DynamicTime(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        StunBuffCfg_DynamicTime dtsbc = cfg as StunBuffCfg_DynamicTime;
        int argsTime = (int)args[0];
        
        // 如果小于最小时间就返回最小时间
        // 如果大于最大时间就返回最大时间
        // 如果在中间就返回原值
        if(argsTime < dtsbc.minStunTime) {
            argsTime = dtsbc.minStunTime;
        }
        else if(argsTime > dtsbc.maxStunTime) {
            argsTime = dtsbc.maxStunTime;
        }
        else
        {
            // TODO 如果想模拟曲线 就该在这里写
        }
        cfg.buffDuration = argsTime;
    }

    protected override void Start() {
        base.Start();

        owner.StunnedCount += 1;
    }

    protected override void End() {
        base.End();

        owner.StunnedCount -= 1;
    }
}