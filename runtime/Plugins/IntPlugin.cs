using System;
using GT.Tweening.Options;

namespace GT.Tweening;

public class IntPlugin : ABSTweenPlugin<int, int, NoOptions>
{
    public override void Reset(TweenerCore<int, int, NoOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<int, int, NoOptions> t, bool isRelative)
    {
        var endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        t.setter(t.startValue);
    }

    public override void SetFrom(TweenerCore<int, int, NoOptions> t, int fromValue, bool setImmediately, bool isRelative)
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

    public override int ConvertToStartValue(TweenerCore<int, int, NoOptions> t, int value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<int, int, NoOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<int, int, NoOptions> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, int changeValue)
    {
        var speedBasedDuration = (float)changeValue / unitsXSecond;
        if (speedBasedDuration < 0.0)
            speedBasedDuration = -speedBasedDuration;
        return speedBasedDuration;
    }

    public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<int> getter, DOSetter<int> setter, float elapsed,
        int startValue, int changeValue,
        float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue += changeValue * (t.isComplete ? t.completedLoops - 1 : t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += changeValue * (t.loopType == LoopType.Incremental ? t.loops : 1) *
                          (t.sequenceParent.isComplete ? t.sequenceParent.completedLoops - 1 : t.sequenceParent.completedLoops);
        setter((int)Math.Round((double)startValue + (double)changeValue *
            (double)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
    }
}