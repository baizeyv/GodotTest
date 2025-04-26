using Godot;
using GT.Tweening.Options;

namespace GT.Tweening;

public class CirclePlugin : ABSTweenPlugin<Vector2, Vector2, CircleOptions>
{
    public override void Reset(TweenerCore<Vector2, Vector2, CircleOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<Vector2, Vector2, CircleOptions> t, bool isRelative)
    {
        if (!t.plugOptions.initialized)
        {
            t.startValue = t.getter();
            t.plugOptions.Initialize(t.startValue, t.endValue);
        }

        float endValueDegrees = t.plugOptions.endValueDegrees;
        t.plugOptions.endValueDegrees = t.plugOptions.startValueDegrees;
        t.plugOptions.startValueDegrees = isRelative ? t.plugOptions.endValueDegrees + endValueDegrees : endValueDegrees;
        t.startValue = this.GetPositionOnCircle(t.plugOptions, t.plugOptions.startValueDegrees);
        t.setter(t.startValue);
    }

    public override void SetFrom(TweenerCore<Vector2, Vector2, CircleOptions> t, Vector2 fromValue, bool setImmediately, bool isRelative)
    {
        if (!t.plugOptions.initialized)
        {
            t.startValue = t.getter();
            t.plugOptions.Initialize(t.startValue, t.endValue);
        }

        float x = fromValue.X;
        if (isRelative)
        {
            float startValueDegrees = t.plugOptions.startValueDegrees;
            t.plugOptions.endValueDegrees += startValueDegrees;
            x += startValueDegrees;
        }

        t.plugOptions.startValueDegrees = x;
        t.startValue = this.GetPositionOnCircle(t.plugOptions, x);
        if (!setImmediately)
            return;
        t.setter(t.startValue);
    }


    public static ABSTweenPlugin<Vector2, Vector2, CircleOptions> Get()
    {
        return PluginsManager.GetCustomPlugin<CirclePlugin, Vector2, Vector2, CircleOptions>();
    }

    public override Vector2 ConvertToStartValue(TweenerCore<Vector2, Vector2, CircleOptions> t, Vector2 value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<Vector2, Vector2, CircleOptions> t)
    {
        if (!t.plugOptions.initialized)
            t.plugOptions.Initialize(t.startValue, t.endValue);
        t.plugOptions.endValueDegrees += t.plugOptions.startValueDegrees;
    }

    public override void SetChangeValue(TweenerCore<Vector2, Vector2, CircleOptions> t)
    {
        if (!t.plugOptions.initialized)
            t.plugOptions.Initialize(t.startValue, t.endValue);
        t.changeValue = new Vector2(t.plugOptions.endValueDegrees - t.plugOptions.startValueDegrees, 0.0f);
    }

    public override float GetSpeedBasedDuration(CircleOptions options, float unitsXSecond, Vector2 changeValue)
    {
        return changeValue.X / unitsXSecond;
    }

    public override void EvaluateAndApply(CircleOptions options, Tween t, bool isRelative, DOGetter<Vector2> getter, DOSetter<Vector2> setter,
        float elapsed, Vector2 startValue,
        Vector2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        float startValueDegrees = options.startValueDegrees;
        if (t.loopType == LoopType.Incremental)
            startValueDegrees += changeValue.X * (t.isComplete ? (float)(t.completedLoops - 1) : (float)t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValueDegrees += (float)((double)changeValue.X * (t.loopType == LoopType.Incremental ? (double)t.loops : 1.0) *
                                         (t.sequenceParent.isComplete
                                             ? (double)(t.sequenceParent.completedLoops - 1)
                                             : (double)t.sequenceParent.completedLoops));
        float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
        setter(this.GetPositionOnCircle(options, startValueDegrees + changeValue.X * num));
    }

    public Vector2 GetPositionOnCircle(CircleOptions options, float degrees)
    {
        Vector2 pointOnCircle = GOTweenUtils.GetPointOnCircle(options.center, options.radius, degrees);
        if (options.snapping)
        {
            pointOnCircle.X = Mathf.Round(pointOnCircle.X);
            pointOnCircle.Y = Mathf.Round(pointOnCircle.Y);
        }

        return pointOnCircle;
    }
}