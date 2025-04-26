using System;
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

    private Type _colorType = typeof(Color);
    
    // TODO:
}