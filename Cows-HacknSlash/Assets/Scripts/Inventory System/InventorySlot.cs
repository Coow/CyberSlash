using System;
using UnityEngine;

[Serializable]
public class InventorySlot : ObservableProperties, IInventorySlot
{
    #region Fields

    [SerializeField]
    private int _slotPosition;
    [SerializeField]
    private int _itemId;
    [SerializeField]
    private int _amount;
    [SerializeField]
    private IInventoryItem _item;

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
        protected set
        {
            SetField(ref _itemId, value);
        }
    }

    public int Amount
    {
        get { return _amount; }
        protected set
        {
            SetField(ref _amount, value);
        }
    }

    public IInventoryItem Item
    {
        get { return _item; }
        protected set
        {
            SetField(ref _item, value);
        }
    }

    public bool Empty => _itemId < 0;

    public bool Full => !Empty && _item.MaxStack == _amount;

    #endregion

    #region Methods

    public InventorySlot(int index, int itemId, int amount)
    {
        Index = index;
        Set(itemId, amount);
    }

    public int Add(int amount)
    {
        if (Empty)
        {
            return amount;
        }

        var canTake = _item.MaxStack - _amount;
        var curTake = canTake >= amount ? amount : canTake;
        Amount += curTake;
        return amount - curTake;
    }

    public int Set(int amount)
    {
        if (Empty)
        {
            return amount;
        }

        var curTake = _item.MaxStack >= amount ? amount : _item.MaxStack;
        Amount = curTake;

        if (Amount == 0)
        {
            Unset();
        }

        return amount - curTake;
    }

    public int Set(int itemId, int amount)
    {
        Id = itemId;
        Item = ResourceManager.ItemsById[_itemId];

        return Set(amount);
    }

    public int Remove(int amount)
    {
        if (Empty)
        {
            return amount;
        }

        var curTake = _amount >= amount ? amount : _amount;
        Amount -= curTake;

        if (Amount == 0)
        {
            Unset();
        }

        return amount - curTake;
    }

    public void Unset()
    {
        Set(-1, 0);
    }

    public void Swap(IInventorySlot other)
    {
        Debug.Log("Preforming swap");
        var id = Id;
        var amount = Amount;

        Set(other.Id, other.Amount);

        other.Set(id, amount);
    }

    public static void Swap(IInventorySlot left, IInventorySlot right)
    {
        var leftId = left.Id;
        var leftAmount = left.Amount;

        left.Set(right.Id, right.Amount);

        right.Set(leftId, leftAmount);
    }

    #endregion
}
