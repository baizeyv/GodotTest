using Godot;

namespace GT.Tweening;

public struct Color2(Color ca, Color cb)
{
    public Color ca = ca;
    public Color cb = cb;

    public static Color2 operator +(Color2 c1, Color2 c2)
    {
        return new Color2(c1.ca + c2.ca, c1.cb + c2.cb);
    }

    public static Color2 operator -(Color2 c1, Color2 c2)
    {
        return new Color2(c1.ca - c2.ca, c1.cb - c2.cb);
    }

    public static Color2 operator *(Color2 c1, float f)
    {
        return new Color2(c1.ca * f, c1.cb * f);
    }
}