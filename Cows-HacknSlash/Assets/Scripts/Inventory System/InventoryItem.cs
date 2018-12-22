using System;
using UnityEngine;

[Serializable]
public class InventoryItem : ObservableProperties, IInventoryItem
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
    private SlotType _slot = SlotType.Standard;

    #endregion

    #region Properties

    /// <summary>
    /// The id of the item
    /// </summary>
    public int Id
    {
        get { return _id; }
        set { SetField(ref _id, value); }
    }

    /// <summary>
    /// How much of the item can be piled in a single inventory slot
    /// </summary>
    public int MaxStack
    {
        get { return _maxStack; }
        set { SetField(ref _maxStack, value); }
    }

    /// <summary>
    /// The item's name
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { SetField(ref _name, value); }
    }

    /// <summary>
    /// The description of the item
    /// </summary>
    public string Description
    {
        get { return _description; }
        set { SetField(ref _description, value); }
    }

    /// <summary>
    /// The image name and path
    /// </summary>
    public string Image
    {
        get { return _image; }
        set { SetField(ref _image, value); }
    }

    /// <summary>
    /// The type of slot the item can go in
    /// </summary>
    public SlotType Type
    {
        get { return _slot; }
        set { SetField(ref _slot, value); }
    }
    
    #endregion
}
