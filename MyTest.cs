using System;
using System.Collections;
using Godot;
using GOTween.coroutine.CoroutineYield;

namespace GOTween;

public partial class MyTest : Node
{
    private CoroutineManager cm = new();

    public override void _Ready()
    {
        cm.StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        GD.Print("A " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return new WaitForSeconds(1f);
        GD.Print("B " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return new WaitForFrames(1);
        GD.Print("C " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return null;
        GD.Print("D " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return new WaitForFrames(0);
        GD.Print("E " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return new WaitForSeconds(0);
        GD.Print("F " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return T2();
        GD.Print("I " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
    }

    private IEnumerator T2()
    {
        GD.Print("G " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
        yield return new WaitForSeconds(1f);
        GD.Print("H " + DateTimeOffset.Now.ToLocalTime().ToString("HH:mm:ss.fff"));
    }

    private void Coroutine(float delta)
    {
        cm.OnCoroutine(delta);
    }

    public override void _Process(double delta)
    {
        GD.Print("-");
        CallDeferred(nameof(Coroutine), (float)delta);
    }
}