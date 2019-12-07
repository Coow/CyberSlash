using System;
using System.Collections.Generic;

public abstract class BaseObject<T> : ObservableProperties, IBaseObject where T : class
{
    #region Fields

    private Guid _id;
    private string _name;
    private static int _type;

    #endregion
    
    #region Properties

    public Guid Id
    {
        get
        {
            return _id;
        }
        set
        {
            SetField(ref _id, value);
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            SetField(ref _name, value);
        }
    }

    public int Type => _type;

    public static int ObjectType => _type;

    #endregion

    #region C/Dtor

    protected BaseObject()
    {
        _type = GetType().Name.GetHashCode();
        Id = Guid.NewGuid();
    }
        
    #endregion

    #region Overrides

    public override bool Equals(object obj)
    {
        return Equals(obj as T);
    }

    public abstract bool Equals(T other);

    public bool Equals(IBaseObject other)
    {
        return other != null && other.Id == Id;
    }

    public virtual bool FilterHits(string filter)
    {
        return Name.ContainsString(filter)
            || Id.ToString().ContainsString(filter);
    }

    public override int GetHashCode()
    {
        return 1969571243 + EqualityComparer<Guid>.Default.GetHashCode(_id);
    }

    #endregion
}