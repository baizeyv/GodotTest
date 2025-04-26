using System;
using Godot;

namespace GT.Tweening;

public static class VectorHelper
{
    public static float Distance(Vector2 a, Vector2 b)
    {
        var num1 = a.X - b.X;
        var num2 = a.Y - b.Y;
        return (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
    }
}