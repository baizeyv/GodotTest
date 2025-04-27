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

    public static Quaternion LookRotation(Vector3 forward, Vector3 upwards = default)
    {
        if (upwards == default)
            upwards = Vector3.Up;
        
        // # 确保前向和向上的方向是正交的 (垂直)
        var right = upwards.Cross(forward).Normalized();
        var trueUp = upwards.Cross(right).Normalized();
        
        // # 创建一个Basis对象,将right,trueUp,forward用作三轴
        var basis = new Basis(right, trueUp, forward);

        return basis.GetRotationQuaternion();
    }
}