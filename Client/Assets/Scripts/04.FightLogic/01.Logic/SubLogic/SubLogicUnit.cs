using proto.HOKProtocol;

/// <summary>
/// 辅助类逻辑单元基类
/// Buff and Bullet
/// </summary>
public abstract class SubLogicUnit : LogicUnit
{
    // 来源角色
    public MainLogicUnit source;
    // 所属技能
    protected Skill skill;
    // 延迟生效时间
    protected int delayTime;
    // 延迟时间计数
    protected int delayCounter;
    // 辅助单元状态
    public SubUnitState unitState;
    
    
    public SubLogicUnit(MainLogicUnit source, Skill skill)
    {
        this.source = source;
        this.skill = skill;
    }
    
    public override void LogicInit()
    {
        if (delayTime == 0)
        {
            unitState = SubUnitState.Start;
        }
        else
        {
            delayCounter = delayTime;
            unitState = SubUnitState.Delay;
        }
    }

    public override void LogicTick()
    {
        switch (unitState)
        {
            case SubUnitState.Delay:
                delayCounter -= Configs.ServerLogicFrameIntervelMs;
                if(delayCounter <= 0) {
                    unitState = SubUnitState.Start;
                }
                break;
            case SubUnitState.End:
                End();
                unitState = SubUnitState.None;
                break;
            case SubUnitState.None:
            default:
                break;
        }
    }

    public override void LogicUnInit()
    {
        
    }
    
    protected abstract void Start();
    protected abstract void Tick();
    protected abstract void End();
}

/// <summary>
/// 阶段
/// </summary>
public enum SubUnitState
{
    None,
    Delay,
    Start,
    Tick,
    End
}