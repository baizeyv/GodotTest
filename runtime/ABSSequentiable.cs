namespace GT.Tweening;

/// <summary>
/// * 抽象可顺序化基类
/// </summary>
public abstract class ABSSequentiable
{
    /// <summary>
    /// * 动画类型
    /// </summary>
    internal TweenType tweenType;

    /// <summary>
    /// * 动画序列位置
    /// </summary>
    internal float sequencedPosition;

    /// <summary>
    /// * 动画序列结束位置
    /// </summary>
    internal float sequencedEndPosition;

    /// <summary>
    /// * 动画起始回调
    /// </summary>
    internal TweenCallback onStart;
}