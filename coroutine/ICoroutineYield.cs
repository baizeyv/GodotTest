namespace GOTween.coroutine;

public interface ICoroutineYield
{
    /// <summary>
    /// * 是否完成了
    /// </summary>
    /// <param name="delta"></param>
    /// <returns></returns>
    bool Tick(float delta);
}