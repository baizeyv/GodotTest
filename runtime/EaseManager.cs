using System;

namespace GT.Tweening;

public static class EaseManager
{
    private const float _PiOver2 = 1.57079637f;

    private const float _TwoPi = 6.28318548f;

    public static float Evaluate(Tween t, float time, float duration, float overshootOrAmplitude, float period)
    {
        return Evaluate(t.easeType, t.customEase, time, duration, overshootOrAmplitude, period);
    }

    public static float Evaluate(Ease easeType, EaseFunction customEase, float time, float duration, float overshootOrAmplitude, float period)
    {
        switch (easeType)
        {
            case Ease.Linear:
                return time / duration;
            case Ease.InSine:
                return (float)(-Math.Cos((double)time / (double)duration * _PiOver2) + 1.0);
            case Ease.OutSine:
                return (float)Math.Sin((double)time / (double)duration * _PiOver2);
            case Ease.InOutSine:
                return (float)(-0.5 * (Math.Cos(3.1415927410125732 * (double)time / (double)duration) - 1.0));
            case Ease.InQuad:
                return (time /= duration) * time;
            case Ease.OutQuad:
                return (float)(-(double)(time /= duration) * ((double)time - 2.0));
            case Ease.InOutQuad:
                return (double)(time /= duration * 0.5f) < 1.0 ? 0.5f * time * time : (float)(-0.5 * ((double)--time * ((double)time - 2.0) - 1.0));
            case Ease.InCubic:
                return (time /= duration) * time * time;
            case Ease.OutCubic:
                return (float)((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time * (double)time + 1.0);
            case Ease.InOutCubic:
                return (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time * time
                    : (float)(0.5 * ((double)(time -= 2f) * (double)time * (double)time + 2.0));
            case Ease.InQuart:
                return (time /= duration) * time * time * time;
            case Ease.OutQuart:
                return (float)-((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time * (double)time * (double)time - 1.0);
            case Ease.InOutQuart:
                return (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time * time * time
                    : (float)(-0.5 * ((double)(time -= 2f) * (double)time * (double)time * (double)time - 2.0));
            case Ease.InQuint:
                return (time /= duration) * time * time * time * time;
            case Ease.OutQuint:
                return (float)((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time * (double)time * (double)time *
                    (double)time + 1.0);
            case Ease.InOutQuint:
                return (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time * time * time * time
                    : (float)(0.5 * ((double)(time -= 2f) * (double)time * (double)time * (double)time * (double)time + 2.0));
            case Ease.InExpo:
                return (double)time != 0.0 ? (float)Math.Pow(2.0, 10.0 * ((double)time / (double)duration - 1.0)) : 0.0f;
            case Ease.OutExpo:
                return (double)time == (double)duration ? 1f : (float)(-Math.Pow(2.0, -10.0 * (double)time / (double)duration) + 1.0);
            case Ease.InOutExpo:
                if ((double)time == 0.0)
                    return 0.0f;
                if ((double)time == (double)duration)
                    return 1f;
                return (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * (float)Math.Pow(2.0, 10.0 * ((double)time - 1.0))
                    : (float)(0.5 * (-Math.Pow(2.0, -10.0 * (double)--time) + 2.0));
            case Ease.InCirc:
                return (float)-(Math.Sqrt(1.0 - (double)(time /= duration) * (double)time) - 1.0);
            case Ease.OutCirc:
                return (float)Math.Sqrt(1.0 - (double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time);
            case Ease.InOutCirc:
                return (double)(time /= duration * 0.5f) < 1.0
                    ? (float)(-0.5 * (Math.Sqrt(1.0 - (double)time * (double)time) - 1.0))
                    : (float)(0.5 * (Math.Sqrt(1.0 - (double)(time -= 2f) * (double)time) + 1.0));
            case Ease.InElastic:
                if ((double)time == 0.0)
                    return 0.0f;
                if ((double)(time /= duration) == 1.0)
                    return 1f;
                if ((double)period == 0.0)
                    period = duration * 0.3f;
                float num1;
                if ((double)overshootOrAmplitude < 1.0)
                {
                    overshootOrAmplitude = 1f;
                    num1 = period / 4f;
                }
                else
                    num1 = period / _TwoPi * (float)Math.Asin(1.0 / (double)overshootOrAmplitude);

                return (float)-((double)overshootOrAmplitude * Math.Pow(2.0, 10.0 * (double)--time) *
                                Math.Sin(((double)time * (double)duration - (double)num1) * _TwoPi / (double)period));
            case Ease.OutElastic:
                if ((double)time == 0.0)
                    return 0.0f;
                if ((double)(time /= duration) == 1.0)
                    return 1f;
                if ((double)period == 0.0)
                    period = duration * 0.3f;
                float num2;
                if ((double)overshootOrAmplitude < 1.0)
                {
                    overshootOrAmplitude = 1f;
                    num2 = period / 4f;
                }
                else
                    num2 = period / _TwoPi * (float)Math.Asin(1.0 / (double)overshootOrAmplitude);

                return (float)((double)overshootOrAmplitude * Math.Pow(2.0, -10.0 * (double)time) *
                    Math.Sin(((double)time * (double)duration - (double)num2) * _TwoPi / (double)period) + 1.0);
            case Ease.InOutElastic:
                if ((double)time == 0.0)
                    return 0.0f;
                if ((double)(time /= duration * 0.5f) == 2.0)
                    return 1f;
                if ((double)period == 0.0)
                    period = duration * 0.450000018f;
                float num3;
                if ((double)overshootOrAmplitude < 1.0)
                {
                    overshootOrAmplitude = 1f;
                    num3 = period / 4f;
                }
                else
                    num3 = period / _TwoPi * (float)Math.Asin(1.0 / (double)overshootOrAmplitude);

                return (double)time < 1.0
                    ? (float)(-0.5 * ((double)overshootOrAmplitude * Math.Pow(2.0, 10.0 * (double)--time) *
                                      Math.Sin(((double)time * (double)duration - (double)num3) * _TwoPi / (double)period)))
                    : (float)((double)overshootOrAmplitude * Math.Pow(2.0, -10.0 * (double)--time) *
                        Math.Sin(((double)time * (double)duration - (double)num3) * _TwoPi / (double)period) * 0.5 + 1.0);
            case Ease.InBack:
                return (float)((double)(time /= duration) * (double)time *
                               (((double)overshootOrAmplitude + 1.0) * (double)time - (double)overshootOrAmplitude));
            case Ease.OutBack:
                return (float)((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time *
                    (((double)overshootOrAmplitude + 1.0) * (double)time + (double)overshootOrAmplitude) + 1.0);
            case Ease.InOutBack:
                return (double)(time /= duration * 0.5f) < 1.0
                    ? (float)(0.5 * ((double)time * (double)time *
                                     (((double)(overshootOrAmplitude *= 1.525f) + 1.0) * (double)time - (double)overshootOrAmplitude)))
                    : (float)(0.5 * ((double)(time -= 2f) * (double)time *
                        (((double)(overshootOrAmplitude *= 1.525f) + 1.0) * (double)time + (double)overshootOrAmplitude) + 2.0));
            case Ease.InBounce:
                return Bounce.EaseIn(time, duration, overshootOrAmplitude, period);
            case Ease.OutBounce:
                return Bounce.EaseOut(time, duration, overshootOrAmplitude, period);
            case Ease.InOutBounce:
                return Bounce.EaseInOut(time, duration, overshootOrAmplitude, period);
            case Ease.Flash:
                return Flash.Ease(time, duration, overshootOrAmplitude, period);
            case Ease.InFlash:
                return Flash.EaseIn(time, duration, overshootOrAmplitude, period);
            case Ease.OutFlash:
                return Flash.EaseOut(time, duration, overshootOrAmplitude, period);
            case Ease.InOutFlash:
                return Flash.EaseInOut(time, duration, overshootOrAmplitude, period);
            case Ease.INTERNAL_Zero:
                return 1f;
            case Ease.INTERNAL_Custom:
                return customEase(time, duration, overshootOrAmplitude, period);
            default:
                return (float)(-(double)(time /= duration) * ((double)time - 2.0));
        }
    }

    public static EaseFunction ToEaseFunction(Ease ease)
    {
        switch (ease)
        {
            case Ease.Linear:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => time / duration);
            case Ease.InSine:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)(-Math.Cos((double)time / (double)duration * _PiOver2) + 1.0));
            case Ease.OutSine:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)Math.Sin((double)time / (double)duration * _PiOver2));
            case Ease.InOutSine:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)(-0.5 * (Math.Cos(3.1415927410125732 * (double)time / (double)duration) - 1.0)));
            case Ease.InQuad:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (time /= duration) * time);
            case Ease.OutQuad:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (float)(-(double)(time /= duration) * ((double)time - 2.0)));
            case Ease.InOutQuad:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time
                    : (float)(-0.5 * ((double)--time * ((double)time - 2.0) - 1.0)));
            case Ease.InCubic:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (time /= duration) * time * time);
            case Ease.OutCubic:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time * (double)time + 1.0));
            case Ease.InOutCubic:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time * time
                    : (float)(0.5 * ((double)(time -= 2f) * (double)time * (double)time + 2.0)));
            case Ease.InQuart:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (time /= duration) * time * time * time);
            case Ease.OutQuart:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)-((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time * (double)time * (double)time - 1.0));
            case Ease.InOutQuart:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time * time * time
                    : (float)(-0.5 * ((double)(time -= 2f) * (double)time * (double)time * (double)time - 2.0)));
            case Ease.InQuint:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (time /= duration) * time * time * time * time);
            case Ease.OutQuint:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time * (double)time * (double)time *
                        (double)time + 1.0));
            case Ease.InOutQuint:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (double)(time /= duration * 0.5f) < 1.0
                    ? 0.5f * time * time * time * time * time
                    : (float)(0.5 * ((double)(time -= 2f) * (double)time * (double)time * (double)time * (double)time + 2.0)));
            case Ease.InExpo:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (double)time != 0.0 ? (float)Math.Pow(2.0, 10.0 * ((double)time / (double)duration - 1.0)) : 0.0f);
            case Ease.OutExpo:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (double)time == (double)duration ? 1f : (float)(-Math.Pow(2.0, -10.0 * (double)time / (double)duration) + 1.0));
            case Ease.InOutExpo:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                {
                    if ((double)time == 0.0)
                        return 0.0f;
                    if ((double)time == (double)duration)
                        return 1f;
                    return (double)(time /= duration * 0.5f) < 1.0
                        ? 0.5f * (float)Math.Pow(2.0, 10.0 * ((double)time - 1.0))
                        : (float)(0.5 * (-Math.Pow(2.0, -10.0 * (double)--time) + 2.0));
                });
            case Ease.InCirc:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)-(Math.Sqrt(1.0 - (double)(time /= duration) * (double)time) - 1.0));
            case Ease.OutCirc:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)Math.Sqrt(1.0 - (double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time));
            case Ease.InOutCirc:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (double)(time /= duration * 0.5f) < 1.0
                    ? (float)(-0.5 * (Math.Sqrt(1.0 - (double)time * (double)time) - 1.0))
                    : (float)(0.5 * (Math.Sqrt(1.0 - (double)(time -= 2f) * (double)time) + 1.0)));
            case Ease.InElastic:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                {
                    if ((double)time == 0.0)
                        return 0.0f;
                    if ((double)(time /= duration) == 1.0)
                        return 1f;
                    if ((double)period == 0.0)
                        period = duration * 0.3f;
                    float num;
                    if ((double)overshootOrAmplitude < 1.0)
                    {
                        overshootOrAmplitude = 1f;
                        num = period / 4f;
                    }
                    else
                        num = period / _TwoPi * (float)Math.Asin(1.0 / (double)overshootOrAmplitude);

                    return (float)-((double)overshootOrAmplitude * Math.Pow(2.0, 10.0 * (double)--time) *
                                    Math.Sin(((double)time * (double)duration - (double)num) * _TwoPi / (double)period));
                });
            case Ease.OutElastic:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                {
                    if ((double)time == 0.0)
                        return 0.0f;
                    if ((double)(time /= duration) == 1.0)
                        return 1f;
                    if ((double)period == 0.0)
                        period = duration * 0.3f;
                    float num;
                    if ((double)overshootOrAmplitude < 1.0)
                    {
                        overshootOrAmplitude = 1f;
                        num = period / 4f;
                    }
                    else
                        num = period / _TwoPi * (float)Math.Asin(1.0 / (double)overshootOrAmplitude);

                    return (float)((double)overshootOrAmplitude * Math.Pow(2.0, -10.0 * (double)time) *
                        Math.Sin(((double)time * (double)duration - (double)num) * _TwoPi / (double)period) + 1.0);
                });
            case Ease.InOutElastic:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                {
                    if ((double)time == 0.0)
                        return 0.0f;
                    if ((double)(time /= duration * 0.5f) == 2.0)
                        return 1f;
                    if ((double)period == 0.0)
                        period = duration * 0.450000018f;
                    float num;
                    if ((double)overshootOrAmplitude < 1.0)
                    {
                        overshootOrAmplitude = 1f;
                        num = period / 4f;
                    }
                    else
                        num = period / _TwoPi * (float)Math.Asin(1.0 / (double)overshootOrAmplitude);

                    return (double)time < 1.0
                        ? (float)(-0.5 * ((double)overshootOrAmplitude * Math.Pow(2.0, 10.0 * (double)--time) *
                                          Math.Sin(((double)time * (double)duration - (double)num) * _TwoPi / (double)period)))
                        : (float)((double)overshootOrAmplitude * Math.Pow(2.0, -10.0 * (double)--time) *
                            Math.Sin(((double)time * (double)duration - (double)num) * _TwoPi / (double)period) * 0.5 + 1.0);
                });
            case Ease.InBack:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (float)((double)(time /= duration) * (double)time *
                                                                                                (((double)overshootOrAmplitude + 1.0) * (double)time -
                                                                                                 (double)overshootOrAmplitude)));
            case Ease.OutBack:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    (float)((double)(time = (float)((double)time / (double)duration - 1.0)) * (double)time *
                        (((double)overshootOrAmplitude + 1.0) * (double)time + (double)overshootOrAmplitude) + 1.0));
            case Ease.InOutBack:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (double)(time /= duration * 0.5f) < 1.0
                    ? (float)(0.5 * ((double)time * (double)time *
                                     (((double)(overshootOrAmplitude *= 1.525f) + 1.0) * (double)time - (double)overshootOrAmplitude)))
                    : (float)(0.5 * ((double)(time -= 2f) * (double)time *
                        (((double)(overshootOrAmplitude *= 1.525f) + 1.0) * (double)time + (double)overshootOrAmplitude) + 2.0)));
            case Ease.InBounce:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => Bounce.EaseIn(time, duration, overshootOrAmplitude, period));
            case Ease.OutBounce:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => Bounce.EaseOut(time, duration, overshootOrAmplitude, period));
            case Ease.InOutBounce:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    Bounce.EaseInOut(time, duration, overshootOrAmplitude, period));
            case Ease.Flash:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => Flash.Ease(time, duration, overshootOrAmplitude, period));
            case Ease.InFlash:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => Flash.EaseIn(time, duration, overshootOrAmplitude, period));
            case Ease.OutFlash:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => Flash.EaseOut(time, duration, overshootOrAmplitude, period));
            case Ease.InOutFlash:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) =>
                    Flash.EaseInOut(time, duration, overshootOrAmplitude, period));
            default:
                return (EaseFunction)((time, duration, overshootOrAmplitude, period) => (float)(-(double)(time /= duration) * ((double)time - 2.0)));
        }
    }

    internal static bool IsFlashEase(Ease ease)
    {
        switch (ease)
        {
            case Ease.Flash:
            case Ease.InFlash:
            case Ease.OutFlash:
            case Ease.InOutFlash:
                return true;
            default:
                return false;
        }
    }
}