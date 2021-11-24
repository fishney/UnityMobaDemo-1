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

public abstract class LogicUnit : ILogic
{

    #region Key Properties

    /// 单位名称
    public string unitName;

    public bool isPosChanged;
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
            isPosChanged = true;
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
    /// 逻辑速度，可被buff加速
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
    /// 基础速度，做记录用，逻辑速度结束buff会恢复
    public PEInt moveSpeedBase;
    
    
    
    
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

