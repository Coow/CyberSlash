using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class InventorySlot : ObservableProperties
{
    #region Fields

    [SerializeField]
    private int _slotPosition;
    [SerializeField]
    private int _itemId;
    [SerializeField]
    private int _amount;
    [SerializeField]
    private InventoryItem _item;

    #endregion

    #region Properties

    public int Index
    {
        get { return _slotPosition; }
        set { SetField(ref _slotPosition, value); }
    }

    public int Id
    {
        get { return _itemId; }
        set { SetField(ref _itemId, value); }
    }

    public int Amount
    {
        get { return _amount; }
        set { SetField(ref _amount, value); }
    }

    public InventoryItem Item
    {
        get { return _item; }
        set { SetField(ref _item, value); }
    }
    
    #endregion
}
