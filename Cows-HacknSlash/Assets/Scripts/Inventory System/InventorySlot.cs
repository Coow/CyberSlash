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
    [SerializeField]
    private SlotType _type;

    #endregion

    #region Properties

    /// <summary>
    /// The index of the slot inside the inventory
    /// </summary>
    public int Index
    {
        get { return _slotPosition; }
        set { SetField(ref _slotPosition, value); }
    }

    /// <summary>
    /// The item id inside the slot
    /// </summary>
    public int Id
    {
        get { return _itemId; }
        protected set
        {
            SetField(ref _itemId, value);
        }
    }

    /// <summary>
    /// The mount if items stacked in the slot
    /// </summary>
    public int Amount
    {
        get { return _amount; }
        protected set
        {
            SetField(ref _amount, value);
        }
    }

    /// <summary>
    /// The item object
    /// </summary>
    public IInventoryItem Item
    {
        get { return _item; }
        protected set
        {
            SetField(ref _item, value);
        }
    }

    /// <summary>
    /// The type of the slot
    /// </summary>
    public SlotType Type
    {
        get { return _type; }
        protected set
        {
            SetField(ref _type, value);
        }
    }

    /// <summary>
    /// Is the slot empty
    /// </summary>
    public bool Empty => _itemId < 0;

    /// <summary>
    /// Is the slot full
    /// </summary>
    public bool Full => !Empty && _item.MaxStack == _amount;

    #endregion

    #region Methods

    /// <summary>
    /// The contructor properly intializes the slot with all necessary parameters
    /// </summary>
    /// <param name="index">The slot index in the inventory</param>
    /// <param name="itemId">The id of the contained item</param>
    /// <param name="amount">The amount in the stack</param>
    /// <param name="type">The type of the slot</param>
    public InventorySlot(int index, int itemId, int amount = 1, SlotType type = SlotType.Standard)
    {
        Index = index;
        Type = type;
        Set(itemId, amount);
    }

    /// <summary>
    /// Adds an amount to the slot stack
    /// </summary>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
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

    /// <summary>
    /// Sets the slot stack to an amount
    /// The amount can't go over the item's max stack
    /// </summary>
    /// <param name="amount">The amount to set</param>
    /// <returns>The amount not set</returns>
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

    /// <summary>
    /// Sets the item and the amount inside the slot properly
    /// </summary>
    /// <param name="itemId">The id of the item</param>
    /// <param name="amount">The amount to set</param>
    /// <returns>The amount not set</returns>
    public int Set(int itemId, int amount)
    {
        Id = itemId;
        var item = ResourceManager.ItemsById[_itemId];

        //Only set if slot types match
        if (CanSet(item.Type))
        {
            Item = item;

            return Set(amount);
        }

        return amount;
    }

    /// <summary>
    /// Removes and amount from the slot stack
    /// </summary>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amount not removed</returns>
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

    /// <summary>
    /// Empties the slot
    /// </summary>
    public void Unset()
    {
        Set(Utility.EmptyId, 0);
    }

    /// <summary>
    /// Swaps the content of the slot with another one
    /// </summary>
    /// <param name="other">The other slot to swap the content with</param>
    public void Swap(IInventorySlot other)
    {
        //Only swap if slot types match
        if (!CanSet(other.Item.Type) || !other.CanSet(Item.Type))
        {
            return;
        }
        
        var id = Id;
        var amount = Amount;

        Set(other.Id, other.Amount);

        other.Set(id, amount);
    }

    /// <summary>
    /// Swaps the content of the slot with another one
    /// </summary>
    /// <param name="left">The slot to swap from</param>
    /// <param name="right">The slot to swap to</param>
    public static void Swap(IInventorySlot left, IInventorySlot right)
    {
        //Only swap if slot types match
        if (!left.CanSet(right.Item.Type) || !right.CanSet(left.Item.Type))
        {
            return;
        }

        var leftId = left.Id;
        var leftAmount = left.Amount;

        left.Set(right.Id, right.Amount);

        right.Set(leftId, leftAmount);
    }

    /// <summary>
    /// Check whether the item can be set inside the slot based on item type
    /// </summary>
    /// <param name="itemType">The type of the item</param>
    /// <returns>True if it can, otherwise false</returns>
    public bool CanSet(SlotType itemType)
    {
        return itemType.HasFlag2(Type);
    }

    #endregion
}
