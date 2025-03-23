namespace GOTween.coroutine.CoroutineYield;
using System;
using System.Collections;
using System.Collections.Generic;

public class IndexedLinkedList<T> : ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>, IEnumerable
{
    private readonly LinkedList<T> _list = new();
    private readonly Dictionary<T, LinkedListNode<T>> _nodeMap = new();
    private readonly List<LinkedListNode<T>> _indexList = new(); // O(1) 索引访问

    public int Count => _list.Count;
    public bool IsReadOnly => false;

    // O(1) 头部插入
    public void InsertFirst(T value)
    {
        var node = new LinkedListNode<T>(value);
        _list.AddFirst(node);
        _nodeMap[value] = node;
        _indexList.Insert(0, node); // 维护索引
    }

    // O(1) 末尾插入
    public void Add(T item)
    {
        var node = new LinkedListNode<T>(item);
        _list.AddLast(node);
        _nodeMap[item] = node;
        _indexList.Add(node);
    }

    // O(1) 通过索引获取值
    public T GetByIndex(int index)
    {
        if (index < 0 || index >= _indexList.Count) throw new IndexOutOfRangeException();
        return _indexList[index].Value;
    }

    // O(1) 删除单个元素
    public bool Remove(T item)
    {
        if (_nodeMap.TryGetValue(item, out var node))
        {
            _list.Remove(node);
            _indexList.Remove(node);
            _nodeMap.Remove(item);
            return true;
        }
        return false;
    }

    // O(n) 但高效的 RemoveAll
    public int RemoveAll(Predicate<T> match)
    {
        int count = 0;
        for (int i = _indexList.Count - 1; i >= 0; i--) // 从尾部遍历，避免跳过元素
        {
            var node = _indexList[i];
            if (match(node.Value))
            {
                _list.Remove(node);
                _indexList.RemoveAt(i);
                _nodeMap.Remove(node.Value);
                count++;
            }
        }
        return count;
    }

    public void Clear()
    {
        _list.Clear();
        _nodeMap.Clear();
        _indexList.Clear();
    }

    public bool Contains(T item) => _nodeMap.ContainsKey(item);

    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach (var item in _list)
        {
            array[arrayIndex++] = item;
        }
    }

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
