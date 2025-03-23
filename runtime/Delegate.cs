namespace GT.Tweening;

/// <summary>
/// * 动画回调委托
/// </summary>
public delegate void TweenCallback();

/// <summary>
/// * 带有参数的动画回调委托
/// </summary>
/// <typeparam name="T"></typeparam>
public delegate void TweenCallback<T>(T value);

/// <summary>
/// * 曲线Func方法委托
/// </summary>
public delegate float EaseFunction(float time, float duration, float overshootOrAmplitude, float period);