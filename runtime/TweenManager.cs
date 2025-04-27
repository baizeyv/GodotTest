namespace GT.Tweening;

internal static class TweenManager
{
    // TODO:

    internal static void Despawn(Tween t, bool modifyActiveLists = true)
    {
        if (t.onKill != null)
            Tween.OnTweenCallback(t.onKill, t);
        if (modifyActiveLists)
            RemoveActiveTween(t);
        if (t.isRecyclable)
        {
            switch (t.tweenType)
            {
                case TweenType.Tweener:
                    // TODO:
                    break;
            }
        }
            // TODO:
    }
    
    // TODO:

    private static void RemoveActiveTween(Tween t)
    {
        var activeId = t.activeId;
        // TODO:
    }
    
    // TODO:
}