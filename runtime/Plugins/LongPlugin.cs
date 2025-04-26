using System;
using GT.Tweening.Options;

namespace GT.Tweening;

public class LongPlugin : ABSTweenPlugin<long, long, NoOptions>
{
    public override void Reset(TweenerCore<long, long, NoOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<long, long, NoOptions> t, bool isRelative)
    {
        long endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        t.setter(t.startValue);
    }

    public override void SetFrom(TweenerCore<long, long, NoOptions> t, long fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            long num = t.getter();
            t.endValue += num;
            fromValue += num;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        t.setter(fromValue);
    }

    public override long ConvertToStartValue(TweenerCore<long, long, NoOptions> t, long value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<long, long, NoOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<long, long, NoOptions> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, long changeValue)
    {
        float speedBasedDuration = (float)changeValue / unitsXSecond;
        if ((double)speedBasedDuration < 0.0)
            speedBasedDuration = -speedBasedDuration;
        return speedBasedDuration;
    }

    public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<long> getter, DOSetter<long> setter, float elapsed,
        long startValue, long changeValue,
        float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue += changeValue * (t.isComplete ? (long)(t.completedLoops - 1) : (long)t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += changeValue * (t.loopType == LoopType.Incremental ? (long)t.loops : 1L) * (t.sequenceParent.isComplete
                ? (long)(t.sequenceParent.completedLoops - 1)
                : (long)t.sequenceParent.completedLoops);
        setter((long)Math.Round((double)startValue + (double)changeValue *
            (double)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
    }
}