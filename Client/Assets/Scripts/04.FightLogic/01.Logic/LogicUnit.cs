/****************************************************
    文件：LogicUnit.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using PEMath;
using UnityEngine;

public abstract class LogicUnit : ILogic
{

    #region Key Properties

    private PEVector3 _logicPos;
    /// 逻辑位置
    public PEVector3 LogicPos
    {
        get
        {
            return _logicPos;
        }
        set
        {
            _logicPos = value;
        }
    }
    
    private PEVector3 _logicDir;
    /// 逻辑方向
    public PEVector3 LogicDir
    {
        get
        {
            return _logicDir;
        }
        set
        {
            _logicDir = value;
        }
    }

    private PEInt _logicMoveSpeed;
    /// 逻辑速度
    public PEInt LogicMoveSpeed
    {
        get
        {
            return _logicMoveSpeed;
        }
        set
        {
            _logicMoveSpeed = value;
        }
    }
    /// 基础速度
    private PEInt MoveSpeedBase;
    
    
    
    
    #endregion

    #region Process

    public abstract void LogicInit();
    public abstract void LogicTick();
    public abstract void LogicUnInit();

    #endregion

    
    
}

interface ILogic
{
    void LogicInit();
    void LogicTick();
    void LogicUnInit();
}