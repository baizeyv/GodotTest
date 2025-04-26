using Godot;
using GOTween.coroutine;
using GOTween.coroutine.CoroutineYield;

namespace GT.Tweening;

public static class TweenExtensions
{
    public static void Complete(this Tween t) => t.Complete(false);
    
    public static void Complete(this Tween t, bool withCallbacks)
    {
        // TODO:
    }

    public static T Done<T>(this T t) where T : Tween
    {
        // TODO:
    }

    public static void Flip(this Tween t)
    {
        // TODO:
    }

    public static void ForceInit(this Tween t)
    {
        // TODO:
    }

    public static void Goto(this Tween t, float to, bool andPlay = false)
    {
        // TODO:
    }

    public static void GotoWithCallbacks(this Tween t, float to, bool andPlay = false)
    {
        // TODO:
    }

    private static void DoGoto(Tween t, float to, bool andPlay, bool withCallbacks)
    {
        // TODO:
    }

    public static void Kill(this Tween t, bool complete = false)
    {
        // TODO:
    }

    public static void ManualUpdate(this Tween t, float deltaTime, float unscaledDeltaTime)
    {
        // TODO:
    }

    public static T Pause<T>(this T t) where T : Tween
    {
        // TODO:
    }
    
    public static T Play<T>(this T t) where T : Tween
    {
        // TODO:
    }

    public static void PlayBackwards(this Tween t)
    {
        // TODO:
    }

    public static void PlayForward(this Tween t)
    {
        // TODO:
    }

    public static void Restart(this Tween t, bool includeDelay = true, float changeDelayTo = -1f)
    {
        // TODO:
    }

    public static void Rewind(this Tween t, bool includeDelay = true)
    {
        // TODO:
    }

    public static void SmoothRewind(this Tween t)
    {
        // TODO:
    }

    public static void TogglePause(this Tween t)
    {
        // TODO:
    }

    public static void GotoWaypoint(this Tween t, int waypointIndex, bool andPlay = false)
    {
        // TODO:
    }

    public static ICoroutineYield WaitForCompletion(this Tween t)
    {
        // TODO:
    }
    
    public static ICoroutineYield WaitForRewind(this Tween t)
    {
        // TODO:
    }
    
    public static ICoroutineYield WaitForKill(this Tween t)
    {
        // TODO:
    }
    
    public static ICoroutineYield WaitForElapsedLoops(this Tween t, int elapsedLoops)
    {
        // TODO:
    }
    
    public static ICoroutineYield WaitForPosition(this Tween t, float position)
    {
        // TODO:
    }

    public static CoroutineManager.CoroutineHandle WaitForStart(this Tween t)
    {
        // TODO:
    }

    public static int CompletedLoops(this Tween t)
    {
        // TODO:
    }

    public static float Delay(this Tween t)
    {
        // TODO:
    }

    public static float ElapsedDelay(this Tween t)
    {
        // TODO:
    }

    public static float Duration(this Tween t, bool includeLoops = true)
    {
        // TODO:
    }

    public static float Elapsed(this Tween t, bool includeLoops = true)
    {
        // TODO:
    }

    public static float ElapsedPercentage(this Tween t, bool includeLoops = true)
    {
        // TODO:
    }

    public static float ElapsedDirectionalPercentage(this Tween t)
    {
        // TODO:
    }

    public static bool IsActive(this Tween t) => t is { active: true };

    public static bool IsBackwards(this Tween t)
    {
        // TODO:
    }

    public static bool IsLoopingOrExecutingBackwards(this Tween t)
    {
        // TODO:
    }

    public static bool IsComplete(this Tween t)
    {
        // TODO:
    }

    public static bool IsInitialized(this Tween t)
    {
        // TODO:
    }

    public static bool IsPlaying(this Tween t)
    {
        // TODO:
    }

    public static int Loops(this Tween t)
    {
        // TODO:
    }

    public static Vector3 PathGetPoint(this Tween t, float pathPercentage)
    {
        // TODO:
    }

    public static Vector3[] PathGetDrawPoints(this Tween t, int subdivisionsXSegment = 10)
    {
        // TODO:
    }

    public static float PathLengtth(this Tween t)
    {
        // TODO:
    }
}