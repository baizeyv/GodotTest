namespace GT.Tweening;

/// <summary>
/// * 嵌套动画失败行为
/// </summary>
public enum NestedTweenFailureBehaviour
{
    TryToPreserveSequence, // # 尝试保留队列动画
    KillWholeSequence // # 杀死整个队列动画
}