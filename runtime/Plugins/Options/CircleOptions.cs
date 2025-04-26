using Godot;

namespace GT.Tweening.Options;

public struct CircleOptions : IPlugOptions
{
    public float endValueDegrees;
    public bool relativeCenter;
    public bool snapping;
    internal Vector2 center;
    internal float radius;
    internal float startValueDegrees;
    internal bool initialized;

    public void Reset()
    {
        this.initialized = false;
        this.startValueDegrees = this.endValueDegrees = 0.0f;
        this.relativeCenter = false;
        this.snapping = false;
    }

    public void Initialize(Vector2 startValue, Vector2 endValue)
    {
        this.initialized = true;
        this.center = endValue;
        if (this.relativeCenter)
            this.center = (startValue + this.center);
        this.radius = VectorHelper.Distance(this.center, startValue);
        Vector2 vector2 = (startValue - this.center);
        this.startValueDegrees = Mathf.Atan2(vector2.X, vector2.Y) * 57.29578f;
    }
}