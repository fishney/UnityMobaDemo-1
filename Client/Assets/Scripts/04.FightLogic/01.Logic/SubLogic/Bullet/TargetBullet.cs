using System;
using PEMath;
using PEPhysx;
using UnityEngine;

/// <summary>
/// 目标追踪子弹类型
/// </summary>
public class TargetBullet: Bullet
{
    /// <summary>
    /// 追踪目标
    /// </summary>
    protected MainLogicUnit target;
    protected PEVector3 curveDir = PEVector3.zero;
    
    public Action<MainLogicUnit, object[]> HitTargetCB;
    
    public TargetBullet(MainLogicUnit source, MainLogicUnit target, Skill skill) : base(source, skill)
    {
        this.target = target;
    }
    
    public override void LogicInit() {
        base.LogicInit();

        PEVector3 targetPos = target.LogicPos + new PEVector3(0, target.ud.unitCfg.hitHeight, 0);
        LogicDir = (targetPos - LogicPos).normalized;
        //发射子弹的位置基于中心点做偏移
        LogicPos += LogicDir * (PEInt)cfg.bulletOffset;
    }
    
    #region 弹道显示 Debug part.1
    int tickCount = 0;
    GameObject ghostRoot;
    #endregion
    protected override void Start() {
        base.Start();

        #region 弹道显示 Debug part.2
        /*
        ghostRoot = new GameObject {
            name = "弹道GhostRoot"
        };
        ghostRoot.transform.localPosition = Vector3.zero;
        UnityEngine.Object.Destroy(ghostRoot, 5);
        */
        #endregion
    }
    
    protected override void Tick() {
        base.Tick();

        LogicDir = (target.LogicPos - LogicPos + new PEVector3(0, target.ud.unitCfg.hitHeight, 0)).normalized;
        if(LogicDir == PEVector3.zero) {
            unitState = SubUnitState.End;
            return;
        }
        if(target.unitState == UnitStateEnum.Dead) {
            unitState = SubUnitState.End;
            return;
        }
        LogicDir += curveDir;
        LogicPos += LogicDir * LogicMoveSpeed;
        //SweepVolume算法
        PEVector3 pos = (LogicPos + lastPos) / 2;
        PEVector3 offset = LogicPos - lastPos;
        ColliderConfig volumeCfg = new ColliderConfig {
            mType = ColliderType.Box,
            mPos = pos,
            mSize = new PEVector3 {
                x = offset.magnitude / 2,// 矩形长的一半
                y = 0,
                z = bulletSize,// 矩形宽的一半
            },
            mAxis = new PEVector3[3]
        };
        volumeCfg.mAxis[0] = offset.normalized;
        volumeCfg.mAxis[1] = PEVector3.up;
        volumeCfg.mAxis[2] = PEVector3.Cross(offset, PEVector3.up).normalized;
        PEBoxCollider volumeCollider = new PEBoxCollider(volumeCfg);
        lastPos = LogicPos;

        #region 弹道显示 Debug part.3
        /*
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.SetParent(ghostRoot.transform);
        tickCount += 1;
        go.name = "ghost_" + tickCount;
        go.GetComponent<MeshRenderer>().enabled = false;
        go.transform.position = volumeCfg.mPos.ConvertViewVector3();
        go.transform.right = volumeCfg.mAxis[0].ConvertViewVector3();
        go.transform.up = volumeCfg.mAxis[1].ConvertViewVector3();
        go.transform.forward = volumeCfg.mAxis[2].ConvertViewVector3();
        go.transform.localScale = volumeCfg.mSize.ConvertViewVector3() * 2;
        */
        #endregion

        PEVector3 normal = PEVector3.zero;
        PEVector3 adj = PEVector3.zero;
        if(target.collider.DetectContact(volumeCollider, ref normal, ref adj)) {
            unitState = SubUnitState.End;
        }
    }
    
    protected override void End() {
        base.End();

        //命中目标,产生效果
        if(target.unitState != UnitStateEnum.Dead) {
            HitTargetCB?.Invoke(target, null);
        }
    }
}