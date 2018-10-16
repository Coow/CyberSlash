using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum SlotType
{
    Standard,
    Helmet,
    Chestplate,
    Rings,
    Leggings,
    Boots,
    Weapon
}

[Serializable]
public class InventoryItem : ObservableProperties
{
    #region Fields

    [SerializeField]
    private int _id;
    [SerializeField]
    private int _maxStack;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private string _image;
    [SerializeField]
    private SlotType _slot;

    #endregion

    #region Properties

    public int Id
    {
        get { return _id; }
        set { SetField(ref _id, value); }
    }
    public int MaxStack
    {
        get { return _maxStack; }
        set { SetField(ref _maxStack, value); }
    }
    public string Name
    {
        get { return _name; }
        set { SetField(ref _name, value); }
    }
    public string Description
    {
        get { return _description; }
        set { SetField(ref _description, value); }
    }
    public string Image
    {
        get { return _image; }
        set { SetField(ref _image, value); }
    }
    public SlotType Type
    {
        get { return _slot; }
        set { SetField(ref _slot, value); }
    }
    
    #endregion
}
