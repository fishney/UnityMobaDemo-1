using PEMath;
using UnityEngine;
using cfg;

public class Bullet : SubLogicUnit
{
    /// <summary>
    /// 上一个逻辑帧的位置，用于sweepvolume体积扫描检测算法
    /// </summary>
    protected PEVector3 lastPos;
    /// <summary>
    /// 子弹半径
    /// </summary>
    protected PEInt bulletSize;
    /// <summary>
    /// 子弹配置，用于表现
    /// </summary>
    protected BulletCfg cfg;

    /// <summary>
    /// 子弹表现层
    /// </summary>
    BulletView bulletView;
    
    
    public Bullet(MainLogicUnit source, Skill skill) : base(source, skill)
    {
    }
    
    public override void LogicInit() {
        cfg = skill.skillCfg.bulletCfg;
        bulletSize = (PEInt)cfg.bulletSize;
        LogicMoveSpeed = (PEInt)cfg.bulletSpeed;
        //子弹当前所在逻辑位置
        LogicPos = source.LogicPos + new PEVector3(0, (PEInt)cfg.bulletHeight, 0);
        lastPos = LogicPos;
        delayTime = cfg.bulletDelay;

        base.LogicInit();
    }
    
    public override void LogicTick() {
        base.LogicTick();
        switch(unitState) {
            case SubUnitState.Start:
                Start();
                unitState = SubUnitState.Tick;
                break;
            case SubUnitState.Tick:
                Tick();
                break;
            default:
                break;
        }
    }
    
    protected override void Start() {
        GameObject go = ResSvc.Instance().LoadPrefab("ResBullets/" + cfg.resPath);
        go.name = source.unitName + "_" + cfg.bulletName;
        bulletView = go.GetComponent<BulletView>();
        if(bulletView == null) {
            this.Error("Get bulletview error:" + unitName);
        }
        else {
            bulletView.Init(this);
        }
    }

    protected override void Tick()
    {
        
    }

    protected override void End()
    {
        bulletView.DestroyBullet();
    }
}