using System;
using System.Runtime.CompilerServices;
using Godot;

namespace GT.Tweening;

public class TweenerCore<T1, T2, TPlugOptions> : Tweener where TPlugOptions : struct, IPlugOptions
{
    public T2 startValue;

    public T2 endValue;

    public T2 changeValue;

    public TPlugOptions plugOptions;

    public DOGetter<T1> getter;

    public DOSetter<T1> setter;

    internal ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

    private const string _TxtCantChangeSequencedValues = "You cannot change the values of a tween contained inside a Sequence";

    private Type _colorType;

    internal TweenerCore() : base()
    {
        _colorType = typeof(Color);
        typeOfT1 = typeof(T1);
        typeOfT2 = typeof(T2);
        typeOfTPlugOptions = typeof(TPlugOptions);
        tweenType = TweenType.Tweener;
        Reset();
    }

    internal override bool Validate()
    {
        try
        {
            T1 obj = this.getter();
        }
        catch
        {
            return false;
        }

        return true;
    }

    internal override float UpdateDelay(float elapsed)
    {
        return Tweener.DoUpdateDelay<T1, T2, TPlugOptions>(this, elapsed);
    }

    internal override bool Startup()
    {
        return Tweener.DoStartup<T1, T2, TPlugOptions>(this);
    }

    internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition,
        ProcessMode processMode,
        ProcessNotice processNotice)
    {
        if (this.isInverted)
            useInversePosition = !useInversePosition;
        float elapsed = useInversePosition ? this.duration - this.position : this.position;
        if (GOTween.useSafeMode)
        {
            try
            {
                this.tweenPlugin.EvaluateAndApply(this.plugOptions, (Tween)this, this.isRelative, this.getter, this.setter, elapsed, this.startValue,
                    this.changeValue, this.duration, useInversePosition, newCompletedSteps, processNotice);
            }
            catch (Exception ex)
            {
                if (Debugger.ShouldLogSafeModeCapturedError())
                    Debugger.LogSafeModeCapturedError(
                        $"Target or field is missing/null ({(object)ex.TargetSite}) ► {(object)ex.Message}\n\n{(object)ex.StackTrace}\n\n", (Tween)this);
                GOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.TargetOrFieldMissing);
                return true;
            }
        }
        else
            this.tweenPlugin.EvaluateAndApply(this.plugOptions, (Tween)this, this.isRelative, this.getter, this.setter, elapsed, this.startValue,
                this.changeValue, this.duration, useInversePosition, newCompletedSteps, processNotice);

        return false;
    }

    public override Tweener ChangeStartValue(object newStartValue, float newDuration = -1)
    {
        if (isSequenced)
        {
            Debugger.LogError(_TxtCantChangeSequencedValues, this);
            return this;
        }

        return DoChangeStartValue(this, (T2)newStartValue, newDuration);
    }

    public override Tweener ChangeEndValue(object newEndValue, bool snapStartValue)
    {
        return ChangeEndValue(newEndValue, -1f, snapStartValue);
    }

    public override Tweener ChangeEndValue(object newEndValue, float newDuration = -1, bool snapStartValue = false)
    {
        if (isSequenced)
        {
            Debugger.LogError(_TxtCantChangeSequencedValues, this);
            return this;
        }

        return DoChangeEndValue(this, (T2)newEndValue, newDuration, snapStartValue);
    }

    public override Tweener ChangeValues(object newStartValue, object newEndValue, float newDuration = -1)
    {
        if (isSequenced)
        {
            Debugger.LogError(_TxtCantChangeSequencedValues, this);
            return this;
        }

        return DoChangeValues(this, (T2)newStartValue, (T2)newEndValue, newDuration);
    }

    internal override Tweener SetFrom(bool relative)
    {
        tweenPlugin.SetFrom(this, relative);
        hasManuallySetStartValue = true;
        return this;
    }

    internal sealed override void Reset()
    {
        base.Reset();
        if (this.tweenPlugin != null)
            this.tweenPlugin.Reset(this);
        this.plugOptions.Reset();
        this.getter = (DOGetter<T1>)null;
        this.setter = (DOSetter<T1>)null;
        this.hasManuallySetStartValue = false;
        this.isFromAllowed = true;
    }

    public TweenerCore<T1, T2, TPlugOptions> ChangeStartValue(T2 newStartValue, float newDuration = -1f)
    {
        if (!this.isSequenced)
            return Tweener.DoChangeStartValue<T1, T2, TPlugOptions>(this, newStartValue, newDuration);
        Debugger.LogError(_TxtCantChangeSequencedValues, (Tween)this);
        return this;
    }

    public TweenerCore<T1, T2, TPlugOptions> ChangeEndValue(T2 newEndValue, bool snapStartValue)
    {
        return this.ChangeEndValue(newEndValue, -1f, snapStartValue);
    }

    public TweenerCore<T1, T2, TPlugOptions> ChangeEndValue(
        T2 newEndValue,
        float newDuration = -1f,
        bool snapStartValue = false)
    {
        if (!this.isSequenced)
            return Tweener.DoChangeEndValue<T1, T2, TPlugOptions>(this, newEndValue, newDuration, snapStartValue);
        Debugger.LogError(_TxtCantChangeSequencedValues, (Tween)this);
        return this;
    }

    public TweenerCore<T1, T2, TPlugOptions> ChangeValues(
        T2 newStartValue,
        T2 newEndValue,
        float newDuration = -1f)
    {
        if (!this.isSequenced)
            return Tweener.DoChangeValues<T1, T2, TPlugOptions>(this, newStartValue, newEndValue, newDuration);
        Debugger.LogError(_TxtCantChangeSequencedValues, (Tween)this);
        return this;
    }

    internal Tweener SetFrom(T2 fromValue, bool setImmediately, bool relative)
    {
        this.tweenPlugin.SetFrom(this, fromValue, setImmediately, relative);
        this.hasManuallySetStartValue = true;
        return (Tweener)this;
    }
}