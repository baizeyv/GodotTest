using System;
using GT.Tweening.Options;

namespace GT.Tweening;

public class UintPlugin : ABSTweenPlugin<uint, uint, UintOptions>
{
    public override void Reset(TweenerCore<uint, uint, UintOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<uint, uint, UintOptions> t, bool isRelative)
    {
        uint endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        t.setter(t.startValue);
    }

    public override void SetFrom(TweenerCore<uint, uint, UintOptions> t, uint fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            uint num = t.getter();
            t.endValue += num;
            fromValue += num;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        t.setter(fromValue);
    }

    public override uint ConvertToStartValue(TweenerCore<uint, uint, UintOptions> t, uint value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<uint, uint, UintOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<uint, uint, UintOptions> t)
    {
        t.plugOptions.isNegativeChangeValue = t.endValue < t.startValue;
        t.changeValue = t.plugOptions.isNegativeChangeValue ? t.startValue - t.endValue : t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(UintOptions options, float unitsXSecond, uint changeValue)
    {
        float speedBasedDuration = (float)changeValue / unitsXSecond;
        if ((double)speedBasedDuration < 0.0)
            speedBasedDuration = -speedBasedDuration;
        return speedBasedDuration;
    }

    public override void EvaluateAndApply(UintOptions options, Tween t, bool isRelative, DOGetter<uint> getter, DOSetter<uint> setter, float elapsed,
        uint startValue,
        uint changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
        {
            uint num = (uint)((long)changeValue * (t.isComplete ? (long)(t.completedLoops - 1) : (long)t.completedLoops));
            if (options.isNegativeChangeValue)
                startValue -= num;
            else
                startValue += num;
        }

        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
        {
            uint num = (uint)((long)changeValue * (t.loopType == LoopType.Incremental ? (long)t.loops : 1L) * (t.sequenceParent.isComplete
                ? (long)(t.sequenceParent.completedLoops - 1)
                : (long)t.sequenceParent.completedLoops));
            if (options.isNegativeChangeValue)
                startValue -= num;
            else
                startValue += num;
        }

        uint num1 = (uint)Math.Round((double)changeValue *
                                     (double)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude,
                                         t.easePeriod));
        if (options.isNegativeChangeValue)
            setter(startValue - num1);
        else
            setter(startValue + num1);
    }
}