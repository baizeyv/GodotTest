using System;
using Godot;
using GT.Tweening.Options;

namespace GT.Tweening;

public class Vector2Plugin : ABSTweenPlugin<Vector2, Vector2, VectorOptions>
{
    public override void Reset(TweenerCore<Vector2, Vector2, VectorOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<Vector2, Vector2, VectorOptions> t, bool isRelative)
    {
        Vector2 endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? (t.endValue + endValue) : endValue;
        Vector2 pNewValue = t.endValue;
        switch (t.plugOptions.axisConstraint)
        {
            case AxisConstraint.X:
                pNewValue.X = t.startValue.X;
                break;
            case AxisConstraint.Y:
                pNewValue.Y = t.startValue.Y;
                break;
            default:
                pNewValue = t.startValue;
                break;
        }

        if (t.plugOptions.snapping)
        {
            pNewValue.X = (float)Math.Round((double)pNewValue.X);
            pNewValue.Y = (float)Math.Round((double)pNewValue.Y);
        }

        t.setter(pNewValue);
    }

    public override void SetFrom(TweenerCore<Vector2, Vector2, VectorOptions> t, Vector2 fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            Vector2 vector2 = t.getter();
            TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = t;
            tweenerCore.endValue = tweenerCore.endValue + vector2;
            fromValue = fromValue + vector2;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        Vector2 pNewValue;
        switch (t.plugOptions.axisConstraint)
        {
            case AxisConstraint.X:
                pNewValue = t.getter();
                pNewValue.X = fromValue.X;
                break;
            case AxisConstraint.Y:
                pNewValue = t.getter();
                pNewValue.Y = fromValue.Y;
                break;
            default:
                pNewValue = fromValue;
                break;
        }

        if (t.plugOptions.snapping)
        {
            pNewValue.X = (float)Math.Round((double)pNewValue.X);
            pNewValue.Y = (float)Math.Round((double)pNewValue.Y);
        }

        t.setter(pNewValue);
    }

    public override Vector2 ConvertToStartValue(TweenerCore<Vector2, Vector2, VectorOptions> t, Vector2 value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<Vector2, Vector2, VectorOptions> t)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = t;
        tweenerCore.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<Vector2, Vector2, VectorOptions> t)
    {
        switch (t.plugOptions.axisConstraint)
        {
            case AxisConstraint.X:
                t.changeValue = new Vector2(t.endValue.X - t.startValue.X, 0.0f);
                break;
            case AxisConstraint.Y:
                t.changeValue = new Vector2(0.0f, t.endValue.Y - t.startValue.Y);
                break;
            default:
                t.changeValue = t.endValue - t.startValue;
                break;
        }
    }

    public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector2 changeValue)
    {
        return changeValue.Length() / unitsXSecond;
    }

    public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector2> getter, DOSetter<Vector2> setter,
        float elapsed, Vector2 startValue,
        Vector2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
            startValue = startValue +
                         (changeValue * (t.isComplete ? (float)(t.completedLoops - 1) : (float)t.completedLoops));
        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue = startValue +
                         ((changeValue * (t.loopType == LoopType.Incremental ? (float)t.loops : 1f)) *
                          (t.sequenceParent.isComplete ? (float)(t.sequenceParent.completedLoops - 1) : (float)t.sequenceParent.completedLoops));
        float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
        switch (options.axisConstraint)
        {
            case AxisConstraint.X:
                Vector2 pNewValue1 = getter();
                pNewValue1.X = startValue.X + changeValue.X * num;
                if (options.snapping)
                    pNewValue1.X = (float)Math.Round((double)pNewValue1.X);
                setter(pNewValue1);
                break;
            case AxisConstraint.Y:
                Vector2 pNewValue2 = getter();
                pNewValue2.Y = startValue.Y + changeValue.Y * num;
                if (options.snapping)
                    pNewValue2.Y = (float)Math.Round((double)pNewValue2.Y);
                setter(pNewValue2);
                break;
            default:
                startValue.X += changeValue.X * num;
                startValue.Y += changeValue.Y * num;
                if (options.snapping)
                {
                    startValue.X = (float)Math.Round((double)startValue.X);
                    startValue.Y = (float)Math.Round((double)startValue.Y);
                }

                setter(startValue);
                break;
        }
    }
}