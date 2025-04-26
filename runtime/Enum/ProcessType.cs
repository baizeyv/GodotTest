namespace GT.Tweening;

/// <summary>
/// * 更新类型
/// </summary>
public enum ProcessType
{
    Normal, // # 普通更新
    Late, // # 延迟更新,使用call_deferred()来实现在当前帧最后执行
    Physics, // # 物理更新
    Manual // # 手动更新
}