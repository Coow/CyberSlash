using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBaseObject<T> : ObservablePropertiesMono, IUIBaseObject where T : class, IBaseObject
{
    private T _object;
    private bool _isSelected;

    [SerializeField]
    Color elementNormalColor = new Color(0, 0, 1, 0.25f);
    [SerializeField]
    Color elementSelectedColor = new Color(0, 1, 0, 0.25f);
    public ReactiveButtonUI DeleteButton;
    public Image elementBackground;

    public T Object
    {
        get
        {
            return _object;
        }

        set
        {
            T previous = _object;
            if (SetField(ref _object, value))
            {
                if (previous != null)
                {
                    previous.PropertyChanged -= Object_PropertyChanged;
                }
                _object.PropertyChanged += Object_PropertyChanged;
                UpdateVisuals();
            }
        }
    }

    public IBaseObject BaseObject
    {
        get
        {
            return Object;
        }
        set
        {
            T obj = value as T;
            if (obj != null)
            {
                Object = obj;
            }
        }
    }

    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }

        set
        {
            if (SetField(ref _isSelected, value))
            {
                if (_isSelected)
                {
                    Selected?.Invoke(this);
                }
                UpdateVisuals();
            }
        }
    }

    public Guid Id => _object.Id;

    protected virtual void Start()
    {
        DeleteButton.Clicked += DeleteButton_Clicked;
    }

    private void DeleteButton_Clicked(ReactiveButtonUI sender)
    {
        DeleteRequested?.Invoke(this);
    }

    #region Events

    public event DeleteRequestedEvent DeleteRequested;

    public event SelectedEvent Selected;

    #endregion

    #region Abstract

    protected abstract void Object_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e);

    protected abstract void UpdateVisuals();
    public abstract void CreateNewObject();
    //protected abstract void OnObjectChanged(T oldObject, T newObject);

    #endregion

    #region Overrides

    public override bool Equals(object obj)
    {
        return Equals(obj as IUIBaseObject);
    }

    public bool Equals(IUIBaseObject other)
    {
        return other != null && other.GetHashCode() == GetHashCode();
    }

    public override int GetHashCode()
    {
        return gameObject.GetInstanceID();
    }

    public bool FilterHits(string filter)
    {
        return _object.FilterHits(filter);
    }

    protected void UpdateSelection(bool isSelected)
    {
        elementBackground.color = isSelected ? elementSelectedColor : elementNormalColor;
    }

    public void OnElementClicked()
    {
        IsSelected = true;
    }

    #endregion
}