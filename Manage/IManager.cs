namespace GOTween.Manage;

public interface IManager
{
    void OnProcess(float deltaTime);
    void OnPhysicsProcess(float deltaTime);
    void OnCoroutine(float deltaTime);
    void OnLateProcess(float deltaTime);
    void OnDestroy();
}