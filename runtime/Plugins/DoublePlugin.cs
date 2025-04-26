using GT.Tweening.Options;

namespace GT.Tweening;

public class DoublePlugin : ABSTweenPlugin<double, double, NoOptions>
{
    public override void Reset(TweenerCore<double, double, NoOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<double, double, NoOptions> t, bool isRelative)
    {
        var endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        t.setter(t.startValue);
    }

    public override void SetFrom(TweenerCore<double, double, NoOptions> t, double fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            var num = t.getter();
            t.endValue += num;
            fromValue += num;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        t.setter(fromValue);
    }

    public override double ConvertToStartValue(TweenerCore<double, double, NoOptions> t, double value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<double, double, NoOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<double, double, NoOptions> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, double changeValue)
    {
        var speedBasedDuration = (float)changeValue / unitsXSecond;
        if ((double)speedBasedDuration < 0.0)
            speedBasedDuration = -speedBasedDuration;
        return speedBasedDuration;
    }

    public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<double> getter, DOSetter<double> setter,
        float elapsed, double startValue,
        double changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue += changeValue * (t.isComplete ? (double)(t.completedLoops - 1) : (double)t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += changeValue * (t.loopType == LoopType.Incremental ? (double)t.loops : 1.0) * (t.sequenceParent.isComplete
                ? (double)(t.sequenceParent.completedLoops - 1)
                : (double)t.sequenceParent.completedLoops);
        setter(startValue + changeValue *
            (double)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod));
    }
}