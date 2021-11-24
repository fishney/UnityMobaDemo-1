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

public class ViewUnit : MonoBehaviour
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
    
    // dir
    public bool IsSyncDir;
    
    // rotation
    public Transform RotationRoot;

    private int predictCount;
    protected Vector3 viewTargetPos;

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

    private void Update()
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

    /// 算出旋转角度
    protected Quaternion CalcRotation(Vector3 targetDir)
    {
        return Quaternion.FromToRotation(Vector3.forward, targetDir);
    }

    public void ForcePosSync()
    {
        transform.position = logicUnit.LogicPos.ConvertViewVector3();
    }
}
