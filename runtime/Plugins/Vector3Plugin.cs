using System;
using Godot;
using GT.Tweening.Options;

namespace GT.Tweening;

public class Vector3Plugin : ABSTweenPlugin<Vector3, Vector3, VectorOptions>
{
    public override void Reset(TweenerCore<Vector3, Vector3, VectorOptions> t)
    {
    }

    public override void SetFrom(TweenerCore<Vector3, Vector3, VectorOptions> t, bool isRelative)
    {
        var endValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + endValue : endValue;
        var pNewValue = t.endValue;
        switch (t.plugOptions.axisConstraint)
        {
            case AxisConstraint.X:
                pNewValue.X = t.startValue.X;
                break;
            case AxisConstraint.Y:
                pNewValue.Y = t.startValue.Y;
                break;
            case AxisConstraint.Z:
                pNewValue.Z = t.startValue.Z;
                break;
            default:
                pNewValue = t.startValue;
                break;
        }

        if (t.plugOptions.snapping)
        {
            pNewValue.X = (float)Math.Round(pNewValue.X);
            pNewValue.Y = (float)Math.Round(pNewValue.Y);
            pNewValue.Z = (float)Math.Round(pNewValue.Z);
        }

        t.setter(pNewValue);
    }

    public override void SetFrom(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 fromValue, bool setImmediately, bool isRelative)
    {
        if (isRelative)
        {
            var vector3 = t.getter();
            var tweenerCore = t;
            tweenerCore.endValue += vector3;
            fromValue += vector3;
        }

        t.startValue = fromValue;
        if (!setImmediately)
            return;
        Vector3 pNewValue;
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
            case AxisConstraint.Z:
                pNewValue = t.getter();
                pNewValue.Z = fromValue.Z;
                break;
            default:
                pNewValue = fromValue;
                break;
        }

        if (t.plugOptions.snapping)
        {
            pNewValue.X = (float)Math.Round(pNewValue.X);
            pNewValue.Y = (float)Math.Round(pNewValue.Y);
            pNewValue.Z = (float)Math.Round(pNewValue.Z);
        }
        t.setter(pNewValue);
    }

    public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 value)
    {
        return value;
    }

    public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
    {
        var tweenerCore = t;
        tweenerCore.endValue += t.startValue;
    }

    public override void SetChangeValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
    {
        switch (t.plugOptions.axisConstraint)
        {
            case AxisConstraint.X:
                t.changeValue = new Vector3(t.endValue.X - t.startValue.X, 0f, 0f);
                break;
            case AxisConstraint.Y:
                t.changeValue = new Vector3(0f, t.endValue.Y - t.startValue.Y, 0f);
                break;
            case AxisConstraint.Z:
                t.changeValue = new Vector3(0f, 0f, t.endValue.Z - t.startValue.Z);
                break;
            default:
                t.changeValue = t.endValue - t.startValue;
                break;
        }
    }

    public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector3 changeValue)
    {
        return changeValue.Length() / unitsXSecond;
    }

    public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue,
        Vector3 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, ProcessNotice processNotice)
    {
        if (t.loopType == LoopType.Incremental)
        {
            startValue = startValue + changeValue * (t.isComplete ? (float)(t.completedLoops - 1) : (float)t.completedLoops);
        }

        if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
            startValue += changeValue * (t.loopType == LoopType.Incremental ? (float)t.loops : 1f) * (t.sequenceParent.isComplete
                ? (float)(t.sequenceParent.completedLoops - 1)
                : (float)t.sequenceParent.completedLoops);
        var num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
        switch (options.axisConstraint)
        {
            case AxisConstraint.X:
                var pNewValue1 = getter();
                pNewValue1.X = startValue.X + changeValue.X * num;
                if (options.snapping)
                    pNewValue1.X = (float)Math.Round(pNewValue1.X);
                setter(pNewValue1);
                break;
            case AxisConstraint.Y:
                var pNewValue2 = getter();
                pNewValue2.Y = startValue.Y + changeValue.Y * num;
                if (options.snapping)
                    pNewValue2.Y = (float)Math.Round(pNewValue2.Y);
                setter(pNewValue2);
                break;
            case AxisConstraint.Z:
                var pNewValue3 = getter();
                pNewValue3.Z = startValue.Z + changeValue.Z * num;
                if (options.snapping)
                    pNewValue3.Z = (float)Math.Round(pNewValue3.Z);
                setter(pNewValue3);
                break;
            default:
                startValue.X += changeValue.X * num;
                startValue.Y += changeValue.Y * num;
                startValue.Z += changeValue.Z * num;
                if (options.snapping)
                {
                    startValue.X = (float)Math.Round(startValue.X);
                    startValue.Y = (float)Math.Round(startValue.Y);
                    startValue.Z = (float)Math.Round(startValue.Z);
                }
                setter(startValue);
                break;
        }
    }
}