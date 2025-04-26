using System;
using GT.Tweening.Options;

namespace GT.Tweening;

public class UlongPlugin : ABSTweenPlugin<ulong, ulong, NoOptions>
{
    public override void Reset(TweenerCore<ulong, ulong, NoOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<ulong, ulong, NoOptions> t, bool isRelative)
    {
        ulong endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        t.setter(t.startValue);
    }

    public override void SetFrom(TweenerCore<ulong, ulong, NoOptions> t, ulong fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            ulong num = t.getter();
            t.endValue += num;
            fromValue += num;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        t.setter(fromValue);
    }

    public override ulong ConvertToStartValue(TweenerCore<ulong, ulong, NoOptions> t, ulong value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<ulong, ulong, NoOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<ulong, ulong, NoOptions> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, ulong changeValue)
    {
        float speedBasedDuration = (float)changeValue / unitsXSecond;
        if ((double)speedBasedDuration < 0.0)
            speedBasedDuration = -speedBasedDuration;
        return speedBasedDuration;
    }

    public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<ulong> getter, DOSetter<ulong> setter, float elapsed,
        ulong startValue,
        ulong changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue += changeValue * (t.isComplete ? (ulong)(uint)(t.completedLoops - 1) : (ulong)(uint)t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += (ulong)((long)changeValue * (t.loopType == LoopType.Incremental ? (long)(uint)t.loops : 1L) * (t.sequenceParent.isComplete
                ? (long)(uint)(t.sequenceParent.completedLoops - 1)
                : (long)(uint)t.sequenceParent.completedLoops));
        setter((ulong)((Decimal)startValue + (Decimal)changeValue *
            (Decimal)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
    }
}