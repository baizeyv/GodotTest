namespace GT.Tweening;

/// <summary>
/// 支持各种类型的动画（float、Vector3、Color、Quaternion、Rect、string 等），每种类型的运算逻辑都不一样。
/// 不同的数据类型，由不同的插件类（继承自 ABSTweenPlugin）实现 Tween 的具体计算方式。
/// </summary>
/// <typeparam name="T1">startValue endValue 类型</typeparam>
/// <typeparam name="T2">计算中间值的内部类型</typeparam>
/// <typeparam name="TPlugOptions"></typeparam>
public abstract class ABSTweenPlugin<T1,T2,TPlugOptions> : ITweenPlugin where TPlugOptions : struct, IPlugOptions
{
    public abstract void Reset(TweenerCore<T1, T2, TPlugOptions> t);
    
    /// <summary>
    /// * 用于设置 Tween 的起始值，通常在调用 SetFrom() 或设置相对动画时使用。
    /// </summary>
    /// <param name="t"></param>
    /// <param name="isRelative"></param>
    public abstract void SetFrom(TweenerCore<T1, T2, TPlugOptions> t, bool isRelative);
    
    public abstract void SetFrom(TweenerCore<T1, T2, TPlugOptions> t, T2 fromValue, bool setImmediately, bool isRelative);

    /// <summary>
    /// * 把原始类型 T1 转成插件内部用的类型 T2。
    /// </summary>
    /// <param name="t"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract T2 ConvertToStartValue(TweenerCore<T1, T2, TPlugOptions> t, T1 value);

    /// <summary>
    /// * 如果 Tween 是相对动画（如 .SetRelative()），就把 endValue 变成相对于 startValue 的偏移值。
    /// </summary>
    /// <param name="t"></param>
    public abstract void SetRelativeEndValue(TweenerCore<T1, T2, TPlugOptions> t);

    /// <summary>
    /// * 用来计算 changeValue = endValue - startValue。
    /// </summary>
    /// <param name="t"></param>
    public abstract void SetChangeValue(TweenerCore<T1, T2, TPlugOptions> t);

    /// <summary>
    /// * 用于计算基于速度的 duration。
    /// </summary>
    /// <param name="options"></param>
    /// <param name="unitsXSecond"></param>
    /// <param name="changeValue"></param>
    /// <returns></returns>
    public abstract float GetSpeedBasedDuration(TPlugOptions options, float unitsXSecond, T2 changeValue);

    public abstract void EvaluateAndApply(TPlugOptions options, Tween t, bool isRelative, DOGetter<T1> getter, DOSetter<T1> setter, float elapsed,
        T2 startValue, T2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice);
}