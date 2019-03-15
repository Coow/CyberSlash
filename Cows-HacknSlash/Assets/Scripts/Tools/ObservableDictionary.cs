using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Quick implementation of an observable dictionary
/// </summary>
/// <typeparam name="TKey">The key type</typeparam>
/// <typeparam name="TValue">The value type</typeparam>
public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged
{
    private Dictionary<TKey, TValue> _elements = new Dictionary<TKey, TValue>();

    public TValue this[TKey key]
    {
        get
        {
            return _elements[key];
        }
        set
        {
            Add(key, value);
        }
    }

    public ICollection<TKey> Keys => _elements.Keys;

    public ICollection<TValue> Values => _elements.Values;

    public int Count => _elements.Count;

    public bool IsReadOnly => false;

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            var old = _elements[key];
            _elements[key] = value;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, old, value));
        }
        else
        {
            _elements[key] = value;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
        }
    }

    public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> elements)
    {
        foreach (var item in elements)
        {
            Add(item);
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _elements.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _elements.Contains(item);
    }

    public bool ContainsKey(TKey key)
    {
        return _elements.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    public bool Remove(TKey key)
    {
        if (ContainsKey(key))
        {
            var old = _elements[key];
            _elements.Remove(key);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, old));
            return true;
        }

        return false;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return Remove(item.Key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return _elements.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return GetEnumerator();
    }
}