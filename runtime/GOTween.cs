using System;
using System.Collections.Generic;
using Godot;
using GT.Tweening.Options;

namespace GT.Tweening;

public class GOTween
{
    /// <summary>
    /// * GOTween 版本
    /// </summary>
    public static readonly string Version = "1.0.0";

    /// <summary>
    /// * 是否使用安全模式
    /// </summary>
    public static bool useSafeMode = true;

    /// <summary>
    /// * 安全模式的日志行为
    /// </summary>
    public static SafeModeLogBehaviour safeModeLogBehaviour = SafeModeLogBehaviour.Warning;

    /// <summary>
    /// * 嵌套动画报错时的行为
    /// </summary>
    public static NestedTweenFailureBehaviour nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;

    /// <summary>
    /// * 是否在Godot Console 中显示GOTween的性能报告和日志
    /// </summary>
    public static bool showGodotEditorReport = false;

    /// <summary>
    /// * 时间缩放
    /// </summary>
    public static float timeScale = 1f;

    /// <summary>
    /// * 未放缩的时间比
    /// </summary>
    public static float unscaledTimeScale = 1f;

    /// <summary>
    /// * 是否使用平滑deltaTime
    /// </summary>
    public static bool useSmoothDeltaTime;

    /// <summary>
    /// * 最大的平滑时长
    /// </summary>
    public static float maxSmoothUnscaledTime = 0.15f;

    /// <summary>
    /// * 倒放的回调模式
    /// </summary>
    internal static RewindCallbackMode rewindCallbackMode = RewindCallbackMode.FireIfPositionChanged;

    /// <summary>
    /// * 日志行为 (Level)
    /// </summary>
    private static LogBehaviour _logBehaviour = LogBehaviour.ErrorsOnly;

    /// <summary>
    /// * 要输出时的Func
    /// </summary>
    public static Func<LogType, object, bool> onWillLog;

    /// <summary>
    /// * 是否是debug mode
    /// </summary>
    public static bool debugMode = false;

    private static bool _fooDebugStoreTargetId = true;

    /// <summary>
    /// * 默认的更新类型
    /// </summary>
    public static ProcessType defaultProcessType = ProcessType.Normal;

    public static bool defaultTimeScaleIndependent = false;

    /// <summary>
    /// * 默认的自动播放类型
    /// </summary>
    public static AutoPlay defaultAutoPlay = AutoPlay.All;

    /// <summary>
    /// * 默认是否要自动kill动画
    /// </summary>
    public static bool defaultAutoKill = true;

    /// <summary>
    /// * 默认循环类型
    /// </summary>
    public static LoopType defaultLoopType = LoopType.Restart;

    /// <summary>
    /// * 默认是否可回收
    /// </summary>
    public static bool defaultRecyclable;

    /// <summary>
    /// * 默认曲线
    /// </summary>
    public static Ease defaultEaseType = Ease.OutQuad;

    /// <summary>
    /// * 默认的曲线过冲或振幅
    /// </summary>
    public static float defaultEaseOvershootOrAmplitude = 1.70158f;

    /// <summary>
    /// * 默认部分曲线周期
    /// </summary>
    public static float defaultEasePeriod = 0.0f;

    public static GOTweenNode instance;

    private static bool _foo_isQuitting;

    internal static int maxActiveTweenersReached;

    internal static int maxActiveSequencesReached;
    
    internal static SafeModeReport safeModeReport;
    
    // TODO:

    internal static bool initialized;

    private static int _isQuittingFrame = -1;

    public static LogBehaviour logBehaviour
    {
        get => GOTween._logBehaviour;
        set
        {
            _logBehaviour = value;
            Debugger.SetLogPriority(_logBehaviour);
        }
    }

    public static bool debugStoreTargetId
    {
        get => debugMode && useSafeMode && _fooDebugStoreTargetId;
        set => _fooDebugStoreTargetId = value;
    }

    internal static bool isQuitting
    {
        get
        {
            if (!_foo_isQuitting)
                return false;
            if (Engine.GetFramesDrawn() < 0 || _isQuittingFrame == Engine.GetFramesDrawn())
                return true;
            _foo_isQuitting = false;
            return false;
        }
        set
        {
            _foo_isQuitting = value;
            if (!value)
                return;
            _isQuittingFrame = Engine.GetFramesDrawn();
        }
    }


    public static IGOTweenInit Init(bool? recycleAllByDefault = null, bool? useSafeMode = null, LogBehaviour? logBehaviour = null)
    {
        if (initialized)
            return GOTween.instance;
        // TODO:init arguments (Load)
        return Engine.IsEditorHint() || GOTween.isQuitting ? null : GOTween.Init(rr);
    }

    private static void AutoInit()
    {
        // TODO:
    }

    private static IGOTweenInit Init(GOTweenSettings settings, bool? recycleAllByDefault, bool? useSafeMode, LogBehaviour? logBehaviour)
    {
        initialized = true;
        if (recycleAllByDefault.HasValue)
            defaultRecyclable = recycleAllByDefault.Value;
        if (useSafeMode.HasValue)
            GOTween.useSafeMode = useSafeMode.Value;
        if (logBehaviour.HasValue)
            GOTween.logBehaviour = logBehaviour.Value;
        // TODO:
    }


    public static void SetTweensCapacity(int tweenersCapacity, int sequencesCapacity)
    {
        // TODO:
    }

    public static void Clear(bool destroy = false)
    {
        // TODO:
    }

    internal static void Clear(bool destroy, bool isApplicationQuitting)
    {
        // TODO;
    }

    public static void ClearCachedTweens()
    {
        // TODO:
    }

    public static int Validate()
    {
        // TODO:
    }

    public static void ManualUpdate(float deltaTime, float unscaledDeltaTime)
    {
        // TODO:
    }

    public static TweenerCore<float, float, FloatOptions> To(DOGetter<float> getter, DOSetter<float> setter, float endValue, float duration)
    {
        // TODO:
    }
    
    // TODO:
}