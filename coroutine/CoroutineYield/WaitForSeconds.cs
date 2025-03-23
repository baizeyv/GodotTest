namespace GOTween.coroutine.CoroutineYield;

/// <summary>
/// ! 这里的WaitForSeconds 和Unity中的协程有一点不一样的是当seconds为0时,unity会等待一帧，而这里则直接继续执行，相当于忽略了
/// </summary>
public class WaitForSeconds : ICoroutineYield
{
    /// <summary>
    /// * 剩余时间
    /// </summary>
    private float _remaining;

    public WaitForSeconds(float seconds)
    {
        _remaining = seconds;
    }

    public bool Tick(float delta)
    {
        _remaining -= delta;
        return _remaining <= 0;
    }
}