using System.Collections;
using Godot;

namespace GT.Tweening;

public partial class GOTweenNode : Node, IGOTweenInit
{
    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
    
    // TODO: LateUpdate

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }
    
    // TODO:

    public IGOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
    {
        // TODO:
        throw new System.NotImplementedException();
    }

    internal IEnumerator WaitForCompletion(Tween t)
    {
        while (t.active && !t.isComplete)
        {
            yield return null;
        }
    }

    internal IEnumerator WaitForRewind(Tween t)
    {
        while (t.active && (!t.playedOnce || t.position * (t.completedLoops + 1) > 0))
        {
            yield return null;
        }
    }

    internal IEnumerator WaitForKill(Tween t)
    {
        while (t.active)
        {
            yield return null;
        }
    }

    internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
    {
        while (t.active && t.completedLoops < elapsedLoops)
        {
            yield return null;
        }
    }

    internal IEnumerator WaitForPosition(Tween t, float position)
    {
        while (t.active && t.position * (t.completedLoops + 1) < position)
        {
            yield return null;
        }
    }

    internal IEnumerator WaitForStart(Tween t)
    {
        while (t.active && !t.playedOnce)
        {
            yield return null;
        }
    }

    internal static void Create()
    {
        // TODO:
    }

    internal static void DestroyInstance()
    {
        if (GOTween.instance != null)
            GOTween.instance.QueueFree();
        GOTween.instance = null;
    }

    // TODO:
}