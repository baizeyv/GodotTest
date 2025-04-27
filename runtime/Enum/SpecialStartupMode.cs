namespace GT.Tweening;

/// <summary>
/// * 特别的启动方法 (Tween 在启动时需要做一些特别处理)
/// </summary>
public enum SpecialStartupMode
{
    None,
    SetLookAt, // # 用于类似LookAt这张,需要在启动时动态确定方向
    SetShake, // # 需要在启动时生成随机震动数据
    SetPunch, // # 需要在启动时计算震动数据
    SetCameraShakePosition
}