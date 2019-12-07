using System;
using System.ComponentModel;

public interface IBaseObject : INotifyPropertyChanged, IIdentity, IFilter, IEquatable<IBaseObject>
{
    int Type { get; }
}