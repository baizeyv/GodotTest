using System;
using GT.Tweening.Options;

namespace GT.Tweening;

public class FloatPlugin : ABSTweenPlugin<float, float, FloatOptions>
{
    public override void Reset(TweenerCore<float, float, FloatOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<float, float, FloatOptions> t, bool isRelative)
    {
        var endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        t.setter(!t.plugOptions.snapping ? t.startValue : (float)Math.Round((double)t.startValue));
    }

    public override void SetFrom(TweenerCore<float, float, FloatOptions> t, float fromValue, bool setImmediately, bool isRelative)
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
        t.setter(!t.plugOptions.snapping ? fromValue : (float)Math.Round((double)fromValue));
    }

    public override float ConvertToStartValue(TweenerCore<float, float, FloatOptions> t, float value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<float, float, FloatOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<float, float, FloatOptions> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(FloatOptions options, float unitsXSecond, float changeValue)
    {
        float speedBasedDuration = changeValue / unitsXSecond;
        if ((double)speedBasedDuration < 0.0)
            speedBasedDuration = -speedBasedDuration;
        return speedBasedDuration;
    }

    public override void EvaluateAndApply(FloatOptions options, Tween t, bool isRelative, DOGetter<float> getter, DOSetter<float> setter,
        float elapsed, float startValue,
        float changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue += changeValue * (t.isComplete ? (float)(t.completedLoops - 1) : (float)t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += (float)((double)changeValue * (t.loopType == LoopType.Incremental ? (double)t.loops : 1.0) * (t.sequenceParent.isComplete
                ? (double)(t.sequenceParent.completedLoops - 1)
                : (double)t.sequenceParent.completedLoops));
        setter(!options.snapping
            ? startValue + changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)
            : (float)Math.Round((double)startValue + (double)changeValue *
                (double)EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
    }
}