using System;
using Godot;
using GT.Tweening.Options;

namespace GT.Tweening;

internal static class SpecialPluginsUtils
{
    internal static bool SetLookAt(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
    {
        var vector3 = Vector3.Zero;
        if (t.target is Node2D d2Item)
        {
            // # 2D Node
            vector3 = t.endValue - new Vector3(d2Item.Position.X, d2Item.Position.Y, 0);
        }
        else if (t.target is Control control)
        {
            // # 2D UI Control
            vector3 = t.endValue - new Vector3(control.Position.X, control.Position.Y, 0);
        }
        else if (t.target is Node3D d3Item)
        {
            // # 3D
            vector3 = t.endValue - d3Item.Position;
        }
        switch (t.plugOptions.axisConstraint)
        {
            case AxisConstraint.X:
                vector3.X = 0f;
                break;
            case AxisConstraint.Y:
                vector3.Y = 0f;
                break;
            case AxisConstraint.Z:
                vector3.Z = 0f;
                break;
        }

        var quaternion = VectorHelper.LookRotation(vector3, t.plugOptions.up);
        var eulerAngles = quaternion.GetEuler();
        t.endValue = eulerAngles;
        return true;
    }

    internal static bool SetPunch(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
    {
        Vector3 vector3;
        try
        {
            vector3 = t.getter();
        }
        catch
        {
            return false;
        }

        t.isRelative = t.isSpeedBased = false;
        t.easeType = Ease.OutQuad;
        t.customEase = null;
        var length = t.endValue.Length;
        for (var index = 0; index < length; ++index)
            t.endValue[index] += vector3;
        return true;
    }

    internal static bool SetShake(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
    {
        if (!SetPunch(t))
            return false;
        t.easeType = Ease.Linear;
        return true;
    }

    internal static bool SetCameraShakePosition(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
    {
        if (!SetShake(t))
            return false;
        if (t.target is Camera2D d2Camera)
        {
            var vector3 = t.getter();
            var length = t.endValue.Length;
            for (var index = 0; index < length; ++index)
            {
                var vector32 = t.endValue[index];
                var v = Vector3.Zero;
                t.endValue[index] = d3Camera.Rotation * (vector32 - vector3) + vector3;
            }
            // TODO:
        } else if (t.target is Camera3D d3Camera)
        {
            var vector3 = t.getter();
            var length = t.endValue.Length;
            for (var index = 0; index < length; ++index)
            {
                var vector32 = t.endValue[index];
                // TODO:
                t.endValue[index] = d3Camera.Rotation * (vector32 - vector3) + vector3;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    
    // TODO:
}