namespace GT.Tweening;

public static class Bounce
{
    public static float EaseIn(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
    {
        return 1f - EaseOut(duration - time, duration, -1f, -1f);
    }

    public static float EaseOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
    {
        if ((double)(time /= duration) < 0.36363637447357178)
            return 121f / 16f * time * time;
        if ((double)time < 0.72727274894714355)
            return (float)(121.0 / 16.0 * (double)(time -= 0.545454562f) * (double)time + 0.75);
        return (double)time < 0.90909093618392944
            ? (float)(121.0 / 16.0 * (double)(time -= 0.8181818f) * (double)time + 15.0 / 16.0)
            : (float)(121.0 / 16.0 * (double)(time -= 0.954545438f) * (double)time + 63.0 / 64.0);
    }

    public static float EaseInOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
    {
        return (double)time < (double)duration * 0.5
            ? Bounce.EaseIn(time * 2f, duration, -1f, -1f) * 0.5f
            : (float)((double)Bounce.EaseOut(time * 2f - duration, duration, -1f, -1f) * 0.5 + 0.5);
    }
}