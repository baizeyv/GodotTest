namespace GT.Tweening.Options;

public struct FloatOptions : IPlugOptions
{
    /// <summary>
    /// * 是否使用整数吸附效果 (四舍五入)
    /// </summary>
    public bool snapping;
    
    public void Reset()
    {
        snapping = false;
    }
}