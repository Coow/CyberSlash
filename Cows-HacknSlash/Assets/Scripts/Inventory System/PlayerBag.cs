using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;


public class PlayerBag : ObservableProperties, IInventory
{
    #region Fields

    [SerializeField]
    private int _size;
    [SerializeField]
    private int _freeSlots;
    [SerializeField]
    private IInventorySlot[] _slots;

    #endregion

    #region Properties

    /// <summary>
    /// The number of free slots in the inventory
    /// </summary>
    public int FreeSlots
    {
        get { return _freeSlots; }
        protected set
        {
            SetField(ref _freeSlots, value);
        }
    }

    /// <summary>
    /// The total number of slots in the inventory
    /// </summary>
    public int Size
    {
        get { return _size; }
        protected set
        {
            SetField(ref _size, value);
        }
    }

    #endregion

    #region Events

    /// <summary>
    /// Fired when a slot changes its content
    /// </summary>
    public event SlotContentChangedEventHandler SlotContentChanged;
    
    #endregion
    
    /// <summary>
    /// Iinitalizes the inventory with the specified amound of slots
    /// </summary>
    /// <param name="slotAmount">The amount of slots the inventory contains</param>
    public PlayerBag(int slotAmount)
    {
        _size = slotAmount;
        _freeSlots = slotAmount;
        _slots = new IInventorySlot[slotAmount];
        for (int i = 0; i < slotAmount; i++)
        {
            _slots[i] = new InventorySlot(i, -1, 0);
            _slots[i].PropertyChanged += PlayerBag_PropertyChanged;
        }
    }

    #region Set

    /// <summary>
    /// Sets an item into a slot
    /// </summary>
    /// <param name="slot">The slot</param>
    /// <param name="item">The item</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not set</returns>
    public int Set(IInventorySlot slot, int itemId, int amount)
    {
        return Set(slot.Index, itemId, amount);
    }

    /// <summary>
    /// Seta an item into a slot
    /// </summary>
    /// <param name="slotIndex">The slot number</param>
    /// <param name="itemId">The item id</param>
    /// <param name="amount">The amount</param>
    /// <returns>The amount not set</returns>
    public int Set(int slotIndex, int itemId, int amount)
    {
        var slot = _slots[slotIndex];
        return slot.Set(itemId, amount);
    }

    /// <summary>
    /// Unsets a slot
    /// </summary>
    /// <param name="slotIndex">The slot index</param>
    public void Unset(int slotIndex)
    {
        var slot = _slots[slotIndex];
        slot.Unset();
        FreeSlots++;
    }

    /// <summary>
    /// Unsets a slot
    /// </summary>
    /// <param name="slot">The slot</param>
    public void Unset(IInventorySlot slot)
    {
        Unset(slot.Index);
    }
    #endregion
    
    #region Add
    
    /// <summary>
    /// Adds an amount of items to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    public int Add(IInventoryItem item, int amount)
    {
        return Add(item.Id, amount);
    }

    /// <summary>
    /// Adds an amount of items to the inventory
    /// </summary>
    /// <param name="itemId">The item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    public int Add(int itemId, int amount)
    {

        int left = amount;

        //First look up existing items to fill up amounts
        foreach (var slot in _slots)
        {
            //Ignore 
            //empty slots
            //those with different items
            //full slots
            if (slot.Empty || slot.Id != itemId || slot.Full)
            {
                continue;
            }

            left = slot.Add(left);
        }

        //If there is still something left
        //Try to fill in empty slots
        if (left > 0 && _freeSlots > 0)
        {
            foreach (var slot in _slots)
            {
                //Already filled all that can be
                if (!slot.Empty)
                {
                    continue;
                }

                left = slot.Set(itemId, left);

                FreeSlots--;

                if (_freeSlots <= 0 || left <= 0)
                {
                    break;
                }
            }
        }

        return left;
    }

    #endregion

    #region Remove
    
    /// <summary>
    /// Removes the amount of items from inventory
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amopunt not removed</returns>
    public int Remove(IInventoryItem item, int amount)
    {
        return Remove(item.Id, amount);
    }

    /// <summary>
    /// Removes the amount of items from inventory
    /// </summary>
    /// <param name="itemId">The item to remove</param>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amount not removed</returns>
    public int Remove(int itemId, int amount)
    {
        int left = amount;
        IInventorySlot slot;

        for (int i = _slots.Length - 1 ; i >= 0; i--)
        {
            slot = _slots[i];
            if (slot.Empty || slot.Id != itemId)
            {
                continue;
            }

            left = slot.Remove(left);

            if (slot.Empty)
            {
                FreeSlots++;
            }

            if (left <= 0)
            {
                break;
            }
        }

        return left;
    }

    #endregion

    #region Utility

    /// <summary>
    /// Reacts to a slot changing its content
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The event data</param>
    private void PlayerBag_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        SlotContentChanged?.Invoke(this, new global::SlotContentChanged { Slot = (IInventorySlot)sender });
    }

    /// <summary>
    /// Searches for the amount of items in the inventory
    /// </summary>
    /// <param name="itemId">The item to search for</param>
    /// <returns>The amount found</returns>
    public int GetAmount(int itemId)
    {
        return _slots.Sum(s => s.Id == itemId ? s.Amount : 0);
    }

    /// <summary>
    /// Searches for all available space for the item
    /// </summary>
    /// <param name="itemId">The item</param>
    /// <returns>The amount space available for the item</returns>
    public int GetFreeSpace(int itemId)
    {
        int total = 0;
        int max = ResourceManager.ItemsById[itemId].MaxStack;

        foreach (var slot in _slots)
        {
            if (slot.Id == itemId)
            {
                total += max - slot.Amount;
            }
            else if (slot.Empty)
            {
                total += max;
            }
        }

        return total;
    }

    /// <summary>
    /// Searches for a presence of items inside the inventory
    /// </summary>
    /// <param name="itemId">The item to search for</param>
    /// <returns>True if item was found, otherwise false</returns>
    public bool Contains(int itemId)
    {
        foreach (var slot in _slots)
        {
            if (slot.Id == itemId)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Accessors

    /// <summary>
    /// Gets the slot at the index
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The slot at index</returns>
    public IInventorySlot this[int index]
    {
        get
        {
            return _slots[index];
        }
        set
        {
            SetField(ref _slots[index], value);
        }
    }
    
    #endregion
}