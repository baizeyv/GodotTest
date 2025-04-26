namespace GT.Tweening;

public abstract class Tweener : Tween
{
    /// <summary>
    /// * 是否手动设置初始值
    /// </summary>
    internal bool hasManuallySetStartValue;

    /// <summary>
    /// * 是否允许 From 功能
    /// </summary>
    internal bool isFromAllowed = true;
    
    internal Tweener() {}

    /// <summary>
    /// * 修改起始值
    /// </summary>
    /// <param name="newStartValue"></param>
    /// <param name="newDuration"></param>
    /// <returns></returns>
    public abstract Tweener ChangeStartValue(object newStartValue, float newDuration = -1f);

    /// <summary>
    /// * 修改结束值
    /// </summary>
    /// <param name="newEndValue"></param>
    /// <param name="newDuration"></param>
    /// <param name="snapStartValue"></param>
    /// <returns></returns>
    public abstract Tweener ChangeEndValue(object newEndValue, float newDuration = -1f, bool snapStartValue = false);

    internal abstract Tweener SetFrom(bool relative);

    internal static bool Setup<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, DOGetter<T1> getter,
        DOSetter<T1> setter, T2 endValue, float duration, ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null)
        where TPlugOptions : struct, IPlugOptions
    {
        if (plugin != null)
            t.tweenPlugin = plugin;
        else
        {
            if (t.tweenPlugin == null)
                t.tweenPlugin = PluginsManager.GetDefaultPlugin<T1, T2, TPlugOptions>();
            if (t.tweenPlugin == null)
            {
                Debugger.LogError("No suitable plugin found for this type");
                return false;
            }
        }

        t.getter = getter;
        t.setter = setter;
        t.endValue = endValue;
        t.duration = duration;
        t.autoKill = GOTween.defaultAutoKill;
        t.isRecyclable = GOTween.defaultRecyclable;
        t.easeType = GOTween.defaultEaseType;
        t.easeOvershootOrAmplitude = GOTween.defaultEaseOvershootOrAmplitude;
        t.easePeriod = GOTween.defaultEasePeriod;
        t.loopType = GOTween.defaultLoopType;
        t.isPlaying = GOTween.defaultAutoPlay == AutoPlay.All || GOTween.defaultAutoPlay == AutoPlay.AutoPlayTweeners;
        return true;
    }

    /// <summary>
    /// * 更新延迟
    /// </summary>
    /// <param name="t"></param>
    /// <param name="elapsed"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TPlugOptions"></typeparam>
    /// <returns></returns>
    internal static float DoUpdateDelay<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, float elapsed)
        where TPlugOptions : struct, IPlugOptions
    {
        var delay = t.delay;
        if (elapsed > delay)
        {
            t.elapsedDelay = delay;
            t.delayComplete = true;
            return elapsed - delay; // # 返回延迟溢出时长
        }

        t.elapsedDelay = elapsed;
        return 0f;
    }

    internal static bool DoStartup<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
    {
        t.startupDone = true;
        if (t.specialStartupMode != SpecialStartupMode.None && !Tweener.)
        // TODO:
    }
    // TODO:

    private static void DOStartupDurationBased<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
    {
        if (t.isSpeedBased)
        {
            t.duration = t.tweenPlugin.GetSpeedBasedDuration(t.plugOptions, t.duration, t.changeValue);
        }

        t.fullDuration = t.loops > -1 ? t.duration * (float)t.loops : float.PositiveInfinity;
    }
}