using Godot;

namespace GOTween.coroutine.CoroutineYield;

public class WaitForFrames : ICoroutineYield
{
    /// <summary>
    /// * 剩余帧数
    /// </summary>
    private int _framesRemaining;

    public WaitForFrames(int frames)
    {
        _framesRemaining = frames;
    }

    public bool Tick(float delta)
    {
        _framesRemaining -= delta > 0 ? 1 : 0;
        return _framesRemaining <= 0;
    }
}