using System;
using HOKProtocol;
using PEMath;

/// <summary>
/// 逻辑定时器(基于定点数)
/// </summary>
public class LogicTimer
{
    private bool isActive;
    public bool IsActive {
        private set {
            isActive = value;
        }
        get {
            return isActive;
        }
    }

    // 服务端每一帧的时间(ms)
    PEInt delta;
    // 延迟时长
    PEInt delayTime;
    // 循环周期时长
    PEInt loopTime;
    // 回调函数
    Action cb;
    
    // 延迟计时器
    PEInt callbackCount;

    
    public LogicTimer(Action cb, PEInt delayTime, int loopTime = 0) {
        this.cb = cb;
        this.delayTime = delayTime;
        this.loopTime = loopTime;
        delta = Configs.ServerLogicFrameIntervelMs;
        IsActive = true;
    }
    
    public void TickTimer() {
        callbackCount += delta;
        if(callbackCount >= delayTime && cb != null) {
            cb();
            if(loopTime == 0) {
                IsActive = false;
                cb = null;
            }
            else {
                callbackCount -= delayTime;
                delayTime = loopTime;
            }
        }
    }
}