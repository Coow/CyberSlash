using System;
using System.ComponentModel;
using UnityEngine;

public interface IUIBaseObject : INotifyPropertyChanged, IEquatable<IUIBaseObject>, ISelectable, IDeletable, IFilter
{
    IBaseObject BaseObject { get; set; }
    Guid Id { get; }
    GameObject gameObject { get; }

    void CreateNewObject();
}