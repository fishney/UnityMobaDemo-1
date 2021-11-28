/****************************************************
    文件：ViewUnit.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewUnit : MonoBehaviour
{
    // pos
    /// 是否开启同步
    public bool IsSyncPos;
    /// 是否开启预测
    public bool PredictPos;
    /// 预测补帧最大帧数
    public int PredictMaxCount;
    /// 是否开启平滑
    public bool SmoothPos;
    /// 平滑加速度
    public float viewPosAccer;
    
    // dir 不预测，只平滑
    public bool IsSyncDir;
    public bool SmoothDir;
    public float viewDirAccer;
    /// 角度倍乘器
    public float AngleMultiplier;
    
    // rotation
    public Transform RotationRoot;

    private int predictCount;
    protected Vector3 viewTargetPos;
    protected Vector3 viewTargetDir;

    // datas
    private LogicUnit logicUnit = null;
    

    public virtual void Init(LogicUnit logicUnit)
    {
        this.logicUnit = logicUnit;
        gameObject.name = logicUnit.unitName + "_" + gameObject.name;

        transform.position = logicUnit.LogicPos.ConvertViewVector3();
        
        if (RotationRoot == null)
        {
            // 比如小兵类会没有RotationRoot，简化
            RotationRoot = transform;
        }

        RotationRoot.rotation = CalcRotation(logicUnit.LogicDir.ConvertViewVector3());
    }

    protected virtual void Update()
    {
        if (IsSyncDir)
        {
            UpdateDirection();
        }
        
        if (IsSyncPos)
        {
            UpdatePosition();
        }
    }

    void UpdateDirection()
    {
        if (logicUnit.isDirChanged)
        {
            viewTargetDir = GetUnitViewDir();
            logicUnit.isDirChanged = false;
        }
        // 不进行插值平滑

        if (SmoothDir)
        {
            // 单帧旋转阈值 = 最大转角速度 * 时间
            float threshold = Time.deltaTime * viewDirAccer;
            float angle = Vector3.Angle(RotationRoot.forward, viewTargetDir);
            // 如果不进行下面的角度加成带入线性计算式，那么这里的旋转平滑就和位置平滑一样了。
            // 但是考虑到角度的偏转可能大可能小，所以做一个根据角度偏转大小成正比的角度变化加成量。
            float angleMult = (angle / 180) * AngleMultiplier * Time.deltaTime;

            if (viewTargetDir != Vector3.zero)
            {
                Vector3 interDir = Vector3.Lerp(RotationRoot.forward, viewTargetDir, threshold + angleMult);
                RotationRoot.rotation = CalcRotation(interDir);
            }
        }
        else
        {
            RotationRoot.rotation = CalcRotation(viewTargetDir);
        }
    }
    
    void UpdatePosition()
    {
        if (PredictPos)
        {
            if (logicUnit.isPosChanged)
            {
                // 逻辑帧更新，强制更新画面帧位置
                viewTargetPos = logicUnit.LogicPos.ConvertViewVector3();
                predictCount = 0;
                logicUnit.isPosChanged = false;
            }
            else
            {
                // 进行插值平滑
                if (predictCount > PredictMaxCount)
                {
                    // 预测帧数量超过设定的最大值，直接return
                    return;
                }
                float delta = Time.deltaTime;
                // 预测位置变化 = 逻辑速度 * 逻辑方向 * 时间
                var predictPos = delta * logicUnit.LogicMoveSpeed.RawFloat * logicUnit.LogicDir.ConvertViewVector3();
                viewTargetPos += predictPos;
                ++predictCount;
            }

            if (SmoothPos)
            { 
                // 平滑移动
                transform.position = Vector3.Lerp(transform.position,viewTargetPos,Time.deltaTime * viewPosAccer);
            }
            else
            {
                transform.position = viewTargetPos; 
            }

        }
        else
        {
            ForcePosSync();
        }
    }
    
    public void ForcePosSync()
    {
        transform.position = logicUnit.LogicPos.ConvertViewVector3();
    }
    
    protected virtual Vector3 GetUnitViewDir()
    {
        return logicUnit.LogicDir.ConvertViewVector3();
    }
    
    /// 算出旋转角度
    protected Quaternion CalcRotation(Vector3 targetDir)
    {
        return Quaternion.FromToRotation(Vector3.forward, targetDir);
    }

    public abstract void PlayAni(string aniName);
}
