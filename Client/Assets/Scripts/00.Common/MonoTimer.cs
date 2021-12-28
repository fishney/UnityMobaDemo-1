using System;

/// <summary>
/// CD计时器。一共分3个部分：延迟、回调函数(循环)执行、结束回调(1次or不)执行。
/// </summary>
public class MonoTimer
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

    /// <summary>
    /// 回调函数
    /// </summary>
    private Action<int> cbAction;
    /// <summary>
    /// 时间间隔
    /// </summary>
    private float intervalTime;
    /// <summary>
    /// 循环次数
    /// </summary>
    private int loopCount;
    
    /// <summary>
    /// args:是不是延迟的部分, 当前阶段(延迟or回调执行)进度, 计时器总体目前进度
    /// </summary>
    private Action<bool, float, float> prgAction;
    /// <summary>
    /// 当前阶段(延迟or回调执行)进度
    /// </summary>
    float prgLoopRate = 0;

    /// <summary>
    /// 延迟累计时长
    /// </summary>
    private float delayCounter;
    /// <summary>
    /// 延迟时长
    /// </summary>
    private float delayTime;
    
    /// <summary>
    /// 单次回调累计时长
    /// </summary>
    private float cbCounter;
    /// <summary>
    /// 循环累计次数
    /// </summary>
    private int loopCounter;

    /// <summary>
    /// 结束后回调函数
    /// </summary>
    private Action endAction;

    /// <summary>
    /// 计时器总体累计时长
    /// </summary>
    private float prgCounter;
    /// <summary>
    /// 总体预计时长
    /// </summary>
    private float prgAllTime;
    /// <summary>
    /// 计时器总体进度
    /// </summary>
    float prgAllRate = 0;
    
    public MonoTimer(
        Action<int> cbAction,
        float intervalTime,
        int loopCount = 1,
        Action<bool, float, float> prgAction = null,
        Action endAction = null,
        float delayTime = 0
    ) {
        this.cbAction = cbAction;
        this.intervalTime = intervalTime;
        this.loopCount = loopCount;
        this.prgAction = prgAction;
        this.endAction = endAction;
        this.delayTime = delayTime;

        this.IsActive = true;
        this.prgAllTime = delayTime + intervalTime * loopCount;
    }

    /// <summary>
    /// 驱动计时器运行
    /// </summary>
    /// <param name="delta">间隔时间，单位ms</param>
    public void TickTimer(float delta)
    {
        if(IsActive) {
            if(delayTime > 0 && delayCounter < delayTime) {
                delayCounter += delta;
                if(delayCounter >= delayTime) {
                    Tick(delayCounter - delayTime);
                }
                else {
                    //delay循环进度,传入prgAction
                    prgLoopRate = delayCounter / delayTime;
                    if(prgAllTime > 0) {
                        prgCounter += delta;
                        prgAllRate = prgCounter / prgAllTime;
                    }
                    prgAction?.Invoke(true, prgLoopRate, prgAllRate);
                }
            }
            else {
                Tick(delta);
            }
        }
    }
    
    private void Tick(float delta) {
        cbCounter += delta;
        //当前这次循环进度
        prgLoopRate = cbCounter / intervalTime;
        //所有计时进度（含delayTime）
        if(prgAllTime > 0) {
            prgCounter += delta;
            prgAllRate = prgCounter / prgAllTime;
        }
        prgAction?.Invoke(false, prgLoopRate, prgAllRate);

        if(cbCounter >= intervalTime) {
            ++loopCounter;
            cbAction(loopCounter);
            if(loopCount != 0 && loopCounter >= loopCount) {
                //达到最大循环次数
                DisableTimer();
            }
            else {
                //未达到最大循环次数,将剩余时间累加入下次循环计算中
                cbCounter -= intervalTime;
            }
        }
    }
    
    public void DisableTimer() {
        IsActive = false;
        endAction?.Invoke();

        cbAction = null;
        prgAction = null;
        endAction = null;
    }
}