using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void ListElementSelectedEvent(IUIBaseObject element);

public class ListManagerUI : ObservablePropertiesMono
{
    protected HashSet<IUIBaseObject> _elements = new HashSet<IUIBaseObject>();
    private string _filter;
    private IUIBaseObject _prefab;
    private int _objectType;
    private IUIBaseObject _selectedElement;

    public GameObject Prefab;
    public Transform Container;

    public EditableLabelUi FilterInput;
    public ReactiveButtonUI AddButton;
    public ReactiveButtonUI SaveButton;
    public ReactiveButtonUI LoadButton;

    public IEnumerable<IUIBaseObject> List => _elements.ToList();

    public event ListElementSelectedEvent ElementSelected;
    public IUIBaseObject SelectedElement
    {
        get
        {
            return _selectedElement;
        }

        protected set
        {
            IUIBaseObject previous = _selectedElement;

            if (SetField(ref _selectedElement, value))
            {
                if (previous != null)
                {
                    previous.IsSelected = false;
                }

                ElementSelected?.Invoke(_selectedElement);
            }
        }
    }

    private void Start()
    {
        AddButton.Clicked += AddButton_Clicked;
        SaveButton.Clicked += SaveButton_Clicked;
        LoadButton.Clicked += LoadButton_Clicked;

        FilterInput.ValueChanged += FilterInput_ValueChanged;

        _prefab = Prefab.GetComponent<IUIBaseObject>();
        _prefab.CreateNewObject();
        _objectType = _prefab.BaseObject.Type;
    }

    private void FilterInput_ValueChanged(string newValue)
    {
        Filter = newValue;
    }

    private void LoadButton_Clicked(ReactiveButtonUI sender)
    {
        OnLoadRequested();
    }

    private void SaveButton_Clicked(ReactiveButtonUI sender)
    {
        OnSaveRequested();
    }

    private void AddButton_Clicked(ReactiveButtonUI sender)
    {
        OnAddRequested();
    }

    private void Item_DeleteRequested(object sender)
    {
        Remove((IUIBaseObject)sender);
    }

    public virtual bool Add(IBaseObject element)
    {
        var obj = CreateNewVisualElement();
        obj.BaseObject = element;

        return Add(obj);
    }

    public virtual bool Add(IUIBaseObject element)
    {
        if (_elements.Add(element))
        {
            element.DeleteRequested += Item_DeleteRequested;
            element.Selected += Element_Selected;
            SceneDataManager.instance.Add(element.BaseObject);
            return true;
        }
        return false;
    }

    protected virtual void Element_Selected(object sender)
    {
        SelectedElement = sender as IUIBaseObject;
    }

    public virtual bool Remove(IUIBaseObject element)
    {
        if (_elements.Remove(element))
        {
            element.DeleteRequested -= Item_DeleteRequested;
            SceneDataManager.instance.Remove(element.Id);
            Destroy(element.gameObject);
            return true;
        }
        return false;
    }

    public string Filter
    {
        get
        {
            return _filter;
        }
        set
        {
            if (SetField(ref _filter, value))
            {
                ApplyFilter();
            }
        }
    }

    private void ApplyFilter()
    {
        var emptyString = string.IsNullOrEmpty(Filter);

        foreach (var element in _elements)
        {
            element.gameObject.SetActive(emptyString ? true : element.FilterHits(_filter));
        }
    }

    public void Clear()
    {
        //Removing elements from a looped list will result in an exception
        //Need to make a copy of the list first
        var elements = _elements.ToList();
        foreach (var element in elements)
        {
            //This is needed to ensure all scene objects are properly detached from dependencies and destroyed
            Remove(element);
        }
    }

    public void OnSaveRequested()
    {
        SceneDataManager.instance.Save();
    }

    public void OnLoadRequested()
    {
        Clear();
        SceneDataManager.instance.Load();
        var elements = SceneDataManager.instance.GetByType(_objectType);
        if (elements != null && elements.Count() > 0)
        {
            foreach (var item in elements.OrderBy(e => e.Id))
            {
                Add(item);
            }
        }
    }

    public void OnAddRequested()
    {
        var obj = CreateNewVisualElement();
        obj.CreateNewObject();

        Add(obj);
    }

    public IUIBaseObject CreateNewVisualElement()
    {
        return Instantiate(Prefab.gameObject, Container).GetComponent<IUIBaseObject>();
    }
}