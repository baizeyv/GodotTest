namespace GT.Tweening.Options;

public struct UintOptions : IPlugOptions
{
    public bool isNegativeChangeValue;

    public void Reset()
    {
        isNegativeChangeValue = false;
    }
}