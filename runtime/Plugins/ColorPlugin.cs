using Godot;
using GT.Tweening.Options;

namespace GT.Tweening;

public class ColorPlugin : ABSTweenPlugin<Color, Color, ColorOptions>
{
    public override void Reset(TweenerCore<Color, Color, ColorOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<Color, Color, ColorOptions> t, bool isRelative)
    {
        Color endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? (t.endValue + endValue) : endValue;
        Color pNewValue = t.endValue;
        if (!t.plugOptions.alphaOnly)
            pNewValue = t.startValue;
        else
            pNewValue.A = t.startValue.A;
        t.setter(pNewValue);
    }

    public override void SetFrom(TweenerCore<Color, Color, ColorOptions> t, Color fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            Color color = t.getter();
            TweenerCore<Color, Color, ColorOptions> tweenerCore = t;
            tweenerCore.endValue = (tweenerCore.endValue + color);
            fromValue = (fromValue + color);
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        Color pNewValue = fromValue;
        if (t.plugOptions.alphaOnly)
        {
            pNewValue = t.getter();
            pNewValue.A = fromValue.A;
        }

        t.setter(pNewValue);
    }

    public override Color ConvertToStartValue(TweenerCore<Color, Color, ColorOptions> t, Color value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<Color, Color, ColorOptions> t)
    {
        TweenerCore<Color, Color, ColorOptions> tweenerCore = t;
        tweenerCore.endValue = (tweenerCore.endValue + t.startValue);
    }

    public override void SetChangeValue(TweenerCore<Color, Color, ColorOptions> t)
    {
        t.changeValue = (t.endValue - t.startValue);
    }

    public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color changeValue)
    {
        return 1f / unitsXSecond;
    }

    public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color> getter, DOSetter<Color> setter,
        float elapsed, Color startValue,
        Color changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue = (startValue +
                          (changeValue * (t.isComplete ? (float)(t.completedLoops - 1) : (float)t.completedLoops)));
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue = (startValue +
                          ((changeValue * (t.loopType == LoopType.Incremental ? (float)t.loops : 1f)) *
                           (t.sequenceParent.isComplete ? (float)(t.sequenceParent.completedLoops - 1) : (float)t.sequenceParent.completedLoops)));
        float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
        if (!options.alphaOnly)
        {
            startValue.R += changeValue.R * num;
            startValue.G += changeValue.G * num;
            startValue.B += changeValue.B * num;
            startValue.A += changeValue.A * num;
            setter(startValue);
        }
        else
        {
            var pNewValue = getter();
            pNewValue.A = startValue.A + changeValue.A * num;
            setter(pNewValue);
        }
    }
}