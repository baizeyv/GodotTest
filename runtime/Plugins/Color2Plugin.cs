using GT.Tweening.Options;

namespace GT.Tweening;

public class Color2Plugin : ABSTweenPlugin<Color2, Color2, ColorOptions>
{
    public override void Reset(TweenerCore<Color2, Color2, ColorOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<Color2, Color2, ColorOptions> t, bool isRelative)
    {
        Color2 endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = !isRelative
            ? new Color2(endValue.ca, endValue.cb)
            : new Color2((t.endValue.ca + endValue.ca), (t.endValue.cb + endValue.cb));
        Color2 pNewValue = t.endValue;
        if (!t.plugOptions.alphaOnly)
        {
            pNewValue = t.startValue;
        }
        else
        {
            pNewValue.ca.A = t.startValue.ca.A;
            pNewValue.cb.A = t.startValue.cb.A;
        }

        t.setter(pNewValue);
    }

    public override void SetFrom(TweenerCore<Color2, Color2, ColorOptions> t, Color2 fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            Color2 color2 = t.getter();
            t.endValue += color2;
            fromValue += color2;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        Color2 pNewValue = fromValue;
        if (t.plugOptions.alphaOnly)
        {
            pNewValue = t.getter();
            pNewValue.ca.A = fromValue.ca.A;
            pNewValue.cb.A = fromValue.cb.A;
        }

        t.setter(pNewValue);
    }

    public override Color2 ConvertToStartValue(TweenerCore<Color2, Color2, ColorOptions> t, Color2 value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<Color2, Color2, ColorOptions> t)
    {
        t.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<Color2, Color2, ColorOptions> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color2 changeValue)
    {
        return 1f / unitsXSecond;
    }

    public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color2> getter, DOSetter<Color2> setter,
        float elapsed, Color2 startValue,
        Color2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue += changeValue * (t.isComplete ? (float)(t.completedLoops - 1) : (float)t.completedLoops);
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += changeValue * (t.loopType == LoopType.Incremental ? (float)t.loops : 1f) * (t.sequenceParent.isComplete
                ? (float)(t.sequenceParent.completedLoops - 1)
                : (float)t.sequenceParent.completedLoops);
        float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
        if (!options.alphaOnly)
        {
            startValue.ca.R += changeValue.ca.R * num;
            startValue.ca.G += changeValue.ca.G * num;
            startValue.ca.B += changeValue.ca.B * num;
            startValue.ca.A += changeValue.ca.A * num;
            startValue.cb.R += changeValue.cb.R * num;
            startValue.cb.G += changeValue.cb.G * num;
            startValue.cb.B += changeValue.cb.B * num;
            startValue.cb.A += changeValue.cb.A * num;
            setter(startValue);
        }
        else
        {
            Color2 pNewValue = getter();
            pNewValue.ca.A = startValue.ca.A + changeValue.ca.A * num;
            pNewValue.cb.A = startValue.cb.A + changeValue.cb.A * num;
            setter(pNewValue);
        }
    }
}