using System;
using Godot;

namespace GT.Tweening;

public static class GOTweenUtils
{
    public static Vector2 GetPointOnCircle(Vector2 center, float radius, float degrees)
    {
        degrees = 90f - degrees;
        var num = degrees * ((float)Math.PI / 180f);
        return (center + (new Vector2(Mathf.Cos(num), Mathf.Sin(num)) * radius));
    }
    
    // TODO:
}