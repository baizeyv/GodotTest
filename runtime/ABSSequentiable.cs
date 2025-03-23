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

    internal float sequencedPosition;

    internal float sequencedEndPosition;

    internal TweenCallback onStart;
}