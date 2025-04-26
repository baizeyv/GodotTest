using System;

namespace GT.Tweening;

/// <summary>
/// * 补间动画抽象基类
/// </summary>
public abstract class Tween : ABSSequentiable
{
    /// <summary>
    /// * 时间缩放
    /// </summary>
    public float timeScale;

    public bool isBackwards;

    /// <summary>
    /// * 是否反转
    /// </summary>
    internal bool isInverted;

    public object id;

    public string stringId;

    public int intId = -999;

    /// <summary>
    /// * 目标对象
    /// </summary>
    public object target;

    /// <summary>
    /// * 逐帧更新类型
    /// </summary>
    internal ProcessType updateType;

    internal bool isIndependentUpdate;

    /// <summary>
    /// * 动画播放时的回调
    /// </summary>
    public TweenCallback onPlay;

    /// <summary>
    /// * 动画暂停时的回调
    /// </summary>
    public TweenCallback onPause;

    /// <summary>
    /// * 动画倒放时的回调
    /// </summary>
    public TweenCallback onRewind;

    /// <summary>
    /// * 动画更新时的回调
    /// </summary>
    public TweenCallback onUpdate;

    public TweenCallback onStepComplete;

    public TweenCallback onComplete;

    public TweenCallback onKill;

    public TweenCallback<int> onWaypointChange;

    internal bool isFrom;

    internal bool isBlendable;

    internal bool isRecyclable;

    internal bool isSpeedBased;

    internal bool autoKill;

    /// <summary>
    /// * 持续时长
    /// </summary>
    internal float duration;

    /// <summary>
    /// * 循环次数
    /// </summary>
    internal int loops;

    /// <summary>
    /// * 循环类型
    /// </summary>
    internal LoopType loopType;

    /// <summary>
    /// * 延迟时长
    /// </summary>
    internal float delay;

    /// <summary>
    /// * 曲线类型
    /// </summary>
    internal Ease easeType;

    /// <summary>
    /// * 自定义曲线
    /// </summary>
    internal EaseFunction customEase;

    public float easeOvershootOrAmplitude;

    public float easePeriod;

    public string debugTargetId;

    internal Type typeOfT1;

    internal Type typeOfT2;

    internal Type typeOfTPlugOptions;

    internal bool isSequenced;

    /// <summary>
    /// * 父动画队列
    /// </summary>
    internal Sequence sequenceParent;

    internal int activeId = -1;

    internal SpecialStartupMode specialStartupMode;

    internal bool creationLocked;

    internal bool startupDone;

    internal float fullDuration;

    internal int completedLoops;

    internal bool isPlaying;

    internal bool isComplete;

    internal float elapsedDelay;

    internal bool delayComplete = true;

    internal int miscInt = -1;

    public bool isRelative { get; internal set; }

    public bool active { get; internal set; }

    public float fullPosition
    {
        get => this.Elapsed();
        set => this.Goto(value, isPlaying);
    }

    public bool hasLoops => loops is -1 or > 1;

    /// <summary>
    /// * 是否播放过一次
    /// </summary>
    public bool playedOnce { get; private set; }

    public float position { get; internal set; }

    internal virtual void Reset()
    {
        timeScale = 1f;
        isBackwards = false;
        id = null;
        stringId = null;
        intId = -999;
        isIndependentUpdate = false;
        onStart = onPlay = onRewind = onUpdate = onComplete = onStepComplete = onKill = null;
        onWaypointChange = null;
        debugTargetId = null;
        target = null;
        isFrom = false;
        isBlendable = false;
        isSpeedBased = false;
        duration = 0f;
        delay = 0f;
        isRelative = false;
        customEase = null;
        isSequenced = false;
        sequenceParent = null;
        specialStartupMode = SpecialStartupMode.None;
        creationLocked = startupDone = playedOnce = false;
        position = fullDuration = completedLoops = 0;
        isPlaying = isComplete = false;
        elapsedDelay = 0f;
        delayComplete = true;
        miscInt = -1;
    }

    internal abstract bool Validate();

    internal virtual float UpdateDelay(float elapsed) => 0f;

    internal abstract bool Startup();

    internal abstract bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps,
        bool useInversePosition, ProcessMode processMode, ProcessNotice processNotice);

    /// <summary>
    /// * 动画跳转
    /// </summary>
    /// <param name="t">要跳转的补间动画实例</param>
    /// <param name="toPosition">要跳转到的位置(秒为单位,或是该tween的自定义时长单位)</param>
    /// <param name="toCompletedLoops">目标循环次数(从0开始)</param>
    /// <param name="processMode">更新模式</param>
    /// <returns></returns>
    internal static bool DoGoto(Tween t, float toPosition, int toCompletedLoops, ProcessMode processMode)
    {
        if (!t.startupDone && !t.Startup())
            // # 该动画尚未启动完成, 确保tween已完成初始化,如果未完成，则调用Startup()方法进行初始化
            return true;
        if (!t.playedOnce && processMode == ProcessMode.Process)
        {
            // # 如果Tween尚未播放过，并且当前是常规更新模式,则触发onStart和onPlay回调
            // # 还没有播放过一次
            t.playedOnce = true; // # 更改标识为已经播放过一次
            if (t.onStart != null)
            {
                // # 调用动画启动的回调
                OnTweenCallback(t.onStart, t);
                if (!t.active) // # 动画未激活
                    return true;
            }

            if (t.onPlay != null)
            {
                // # 调用动画播放的回调
                OnTweenCallback(t.onPlay, t);
                if (!t.active) // # 动画为激活
                    return true;
            }
        }

        var position = t.position; // # 播放位置
        var completedLoops = t.completedLoops; // # 完成循环数量
        t.completedLoops = toCompletedLoops;
        var flag = t.position <= 0.0 && completedLoops <= 0;
        var isComplete = t.isComplete;
        if (t.loops != -1)
            // # 矫正需要指定循环次数的动画是否完成了
            t.isComplete = t.completedLoops == t.loops;
        var newCompletedSteps = 0;
        if (processMode == ProcessMode.Process)
        {
            if (t.isBackwards)
            {
                newCompletedSteps = t.completedLoops < completedLoops
                    ? completedLoops - t.completedLoops
                    : (toPosition > 0.0 || flag ? 0 : 1);
                if (isComplete)
                    --newCompletedSteps;
            }
            else
                newCompletedSteps = t.completedLoops > completedLoops ? t.completedLoops - completedLoops : 0;
        }
        else if (t.tweenType == TweenType.Sequence)
        {
            // # 队列动画
            newCompletedSteps = completedLoops - toCompletedLoops;
            if (newCompletedSteps < 0)
                newCompletedSteps = -newCompletedSteps;
        }

        t.position = toPosition;
        if (t.position > t.duration)
            t.position = t.duration;
        else if (t.position <= 0.0)
            t.position = t.completedLoops > 0 || t.isComplete ? t.duration : 0f;
        var isPlaying = t.isPlaying;
        if (t.isPlaying)
            t.isPlaying = t.isBackwards ? t.completedLoops != 0 || t.position > 0.0 : !t.isComplete;
        var useInversePosition = t.hasLoops && t.loopType == LoopType.Yoyo && (t.position < t.duration
            ? t.completedLoops % 2 != 0
            : t.completedLoops % 2 == 0);
        var processNotice = (flag
            ? 0
            : (t.loopType != LoopType.Restart || t.completedLoops == completedLoops || t.loops != -1
                && t.completedLoops >= t.loops
                    ? (t.position > 0.0 ? 0 : (t.completedLoops <= 0 ? 1 : 0)) : 1)) != 0
                        ? ProcessNotice.RewindStep
                        : ProcessNotice.None;
        if (t.ApplyTween(position, completedLoops, newCompletedSteps, useInversePosition, processMode, processNotice))
            return true;
        if (t.onUpdate != null && processMode != ProcessMode.IgnoreOnProcess)
            OnTweenCallback(t.onUpdate, t);
        if (t.position <= 0.0 && t.completedLoops <= 0 && !flag && t.onRewind != null)
            OnTweenCallback(t.onRewind, t);
        if (newCompletedSteps > 0 && processMode == ProcessMode.Process && t.onStepComplete != null)
        {
            for (var index = 0; index < newCompletedSteps; ++index)
            {
                OnTweenCallback(t.onStepComplete, t);
                if (!t.active)
                    break;
            }
        }

        if (t.isComplete && !isComplete && processMode != ProcessMode.IgnoreOnComplete && t.onComplete != null)
            OnTweenCallback(t.onComplete, t);
        if (!t.isPlaying & isPlaying && (!t.isComplete || !t.autoKill) && t.onPause != null)
            OnTweenCallback(t.onPause, t);
        return t.autoKill && t.isComplete;
    }

    /// <summary>
    /// * 动画回调方法
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    internal static bool OnTweenCallback(TweenCallback callback, Tween t)
    {
        if (GOTween.useSafeMode)
        {
            try
            {
                callback();
            }
            catch (Exception ex)
            {
                if (Debugger.ShouldLogSafeModeCapturedError())
                    Debugger.LogSafeModeCapturedError(
                        $"An error inside a tween callback was taken care of ({ex.TargetSite}) ► {ex.Message}\n\n{ex.StackTrace}\n\n",
                        t);
                GOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.Callback);
                return false;
            }
        }
        else
        {
            callback();
        }

        return true;
    }

    /// <summary>
    /// * 带有参数的动画回调方法
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="t"></param>
    /// <param name="param"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    internal static bool OnTweenCallback<T>(TweenCallback<T> callback, Tween t, T param)
    {
        if (GOTween.useSafeMode)
        {
            // # 处于安全模式 (有安全Log的情况)
            try
            {
                callback(param);
            }
            catch (Exception ex)
            {
                if (Debugger.ShouldLogSafeModeCapturedError())
                {
                    // # 安全模式中需要捕获错误
                    Debugger.LogSafeModeCapturedError(
                        $"An error inside a tween callback was taken care of ({ex.TargetSite}) ► {ex.Message}", t);
                }

                GOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.Callback);
                return false;
            }
        }
        else
        {
            callback(param);
        }

        return true;
    }
}