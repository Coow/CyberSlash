using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

/// <summary>
/// This needs to be put inside the scene that will use it
/// Or the first loaded scene, if multiple scenes will be loaded
/// </summary>
public class SceneDataManager
{
    private ObservableDictionary<Guid, IBaseObject> _elements = new ObservableDictionary<Guid, IBaseObject>();

    public static SceneDataManager instance = new SceneDataManager();

    public INotifyCollectionChanged ObservableElements => _elements;
    public IDictionary<Guid, IBaseObject> Elements => _elements.Values.ToDictionary(v => v.Id);

    /// <summary>
    /// Used to tell the C# compiler no to mark type as beforefieldinit
    /// Avoids error prone C# compiler behaviour
    /// </summary>
    static SceneDataManager() { }

    /// <summary>
    /// Avoid this being instantiated more than once
    /// </summary>
    private SceneDataManager()
    {
        Load();
    }

    public void Add(IBaseObject item)
    {
        _elements[item.Id] = item;
    }

    public void Remove(IBaseObject item)
    {
        Remove(item.Id);
    }

    public void Remove(Guid id)
    {
        _elements.Remove(id);
    }

    public void Save()
    {
        DataManager.SerializeDataToFile(SceneConfiguration.Settings.ObjectsList, _elements);
    }

    public void Load()
    {
        _elements.AddRange(DataManager.DeserializeDataFromFile<ObservableDictionary<Guid, IBaseObject>>(SceneConfiguration.Settings.ObjectsList));
    }

    public int Count => _elements.Count;

    public IEnumerable<IBaseObject> GetByType(int type)
    {
        return _elements?.Where(e => e.Value.Type == type).Select(e => e.Value) ?? new List<IBaseObject>();
    }

    public IBaseObject this[Guid id]
    {
        get
        {
            return _elements[id];
        }
        set
        {
            _elements[id] = value;
        }
    }
}