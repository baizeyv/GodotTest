using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using GOTween.Manage;

namespace GOTween.coroutine.CoroutineYield;

/// <summary>
/// TODO: 单例
/// </summary>
public class CoroutineManager : IManager
{
    public class CoroutineHandle : ICoroutineYield
    {
        public IEnumerator Routine;
        public object CurrentYield;
        public float AccumulatedTime;
        public bool IsDone;
        public bool IsProcessing;
        public CoroutineManager Manager;
        public bool Tick(float delta)
        {
            return IsDone || Manager.ProcessNestedCoroutine(this, delta);
        }
    }

    private readonly IndexedLinkedList<CoroutineHandle> _activeCoroutines = new();
    private readonly Queue<CoroutineHandle> _pendingAdd = new();
    private readonly HashSet<CoroutineHandle> _pendingRemove = new();
    private readonly Queue<CoroutineHandle> _pendingNested = new();

    public CoroutineHandle StartCoroutine(IEnumerator routine)
    {
        var handle = new CoroutineHandle()
        {
            Routine = routine,
            CurrentYield = null,
            AccumulatedTime = 0,
            IsDone = false,
            IsProcessing = false,
            Manager = this
        };
        _pendingAdd.Enqueue(handle);
        return handle;
    }

    private CoroutineHandle StartNestedCoroutine(IEnumerator routine)
    {
        var handle = new CoroutineHandle()
        {
            Routine = routine,
            CurrentYield = null,
            AccumulatedTime = 0,
            IsDone = false,
            IsProcessing = false,
            Manager = this
        };
        _pendingNested.Enqueue(handle);
        return handle;
    }

    public void StopCoroutine(CoroutineHandle handle)
    {
        handle.IsDone = true;
        _pendingRemove.Add(handle);
    }

    private void ProcessPendingOperations()
    {
        while (_pendingNested.Count > 0)
        {
            _activeCoroutines.InsertFirst( _pendingNested.Dequeue());
        }
        while (_pendingAdd.Count > 0)
        {
            _activeCoroutines.Add(_pendingAdd.Dequeue());
        }

        _activeCoroutines.RemoveAll(x => _pendingRemove.Contains(x));
        _pendingRemove.Clear();
    }

    private void UpdateCoroutines(float delta)
    {
        for (var i = 0; i < _activeCoroutines.Count; i++)
        {
            var handle = _activeCoroutines.GetByIndex(i);
            if (handle.IsDone || handle.IsProcessing)
                continue;
            handle.IsProcessing = true;
            ProcessCoroutineStep(handle, delta);
            handle.IsProcessing = false;
        }
    }

    private bool ProcessNestedCoroutine(CoroutineHandle handle, float delta)
    {
        ProcessCoroutineStep(handle, delta);
        return handle.IsDone;
    }

    private void ProcessCoroutineStep(CoroutineHandle handle, float delta)
    {
        while (true)
        {
            // # 检查当前等待条件
            if (handle.CurrentYield is ICoroutineYield yieldCondition)
            {
                if (!yieldCondition.Tick(delta))
                    break;
            } else if (!CheckYieldCondition(handle, delta))
            {
                break;
            }

            // # 推进协程
            try
            {
                var routine = handle.Routine;
                if (routine.MoveNext())
                {
                    var newYield = routine.Current;
                    HandleYieldAssignment(handle, newYield);

                    // # 检查是否可以立即继续
                    if (!CanContinueImmediately(handle.CurrentYield))
                        break;
                }
                else
                {
                    handle.IsDone = true;
                    _pendingRemove.Add(handle);
                    break;
                }
            }
            catch (Exception e)
            {
                GD.Print($"协程异常: {e.Message}");
                handle.IsDone = true;
                _pendingRemove.Add(handle);
                break;
            }
        }
    }

    private void HandleYieldAssignment(CoroutineHandle handle, object newYield)
    {
        // # 处理嵌套协程
        if (newYield is IEnumerator nestedRoutine)
        {
            handle.CurrentYield = StartNestedCoroutine(nestedRoutine);
        }
        else
        {
            handle.CurrentYield = newYield;
            handle.AccumulatedTime = 0;
        }
    }

    private static bool CheckYieldCondition(CoroutineHandle handle, float delta)
    {
        if (handle.CurrentYield == null)
        {
            return true;
        }

        handle.AccumulatedTime += delta;
        switch (handle.CurrentYield)
        {
            case ICoroutineYield customYield:
                return customYield.Tick(delta);
            case float waitSeconds:
                if (handle.AccumulatedTime >= waitSeconds)
                {
                    handle.AccumulatedTime = 0;
                    return true;
                }

                return false;
            case int waitFrames:
                handle.AccumulatedTime += 1;
                return handle.AccumulatedTime >= waitFrames;
            default:
                GD.PrintErr($"不支持的等待类型: {handle.CurrentYield.GetType()}");
                return true;
        }
    }

    private bool CanContinueImmediately(object yieldCondition)
    {
        return yieldCondition switch
        {
            null => false,
            ICoroutineYield cy => cy.Tick(0),
            float f => f <= 0,
            int i => i <= 0,
            _ => false
        };
    }

    private void CleanupCompleted()
    {
        _activeCoroutines.RemoveAll(x => x.IsDone);
    }

    public void OnProcess(float deltaTime)
    {
    }

    public void OnPhysicsProcess(float deltaTime)
    {
    }

    public void OnCoroutine(float deltaTime)
    {
        ProcessPendingOperations();
        UpdateCoroutines(deltaTime);
        CleanupCompleted();
    }

    public void OnLateProcess(float deltaTime)
    {
    }

    public void OnDestroy()
    {
    }
}
