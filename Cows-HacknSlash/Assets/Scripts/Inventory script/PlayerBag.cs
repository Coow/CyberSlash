using System.Linq;
using UnityEngine;

public class PlayerBag : IInventory
{
    #region Fields

    [SerializeField]
    private int _size;
    [SerializeField]
    private int _freeSlots;
    [SerializeField]
    private InventorySlot[] _slots;

    #endregion

    #region Properties

    public int FreeSlots
    {
        get { return _freeSlots; }
    }

    public int Size
    {
        get { return _size; }
    }

    #endregion
    
    /// <summary>
    /// Iinitalizes the inventory with the specified amound of slots
    /// </summary>
    /// <param name="slotAmount">The amount of slots the inventory contains</param>
    public PlayerBag(int slotAmount)
    {
        _size = slotAmount;
        _freeSlots = slotAmount;
        _slots = new InventorySlot[slotAmount];
        for (int i = 0; i < slotAmount; i++)
        {
            _slots[i] = new InventorySlot
            {
                Index = i,
                Amount = 0,
                Id = -1
            };
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
    public int SetItem(InventorySlot slot, InventoryItem item, int amount)
    {
        slot.Id = item.Id;
        slot.Item = item;
        var curTake = item.MaxStack >= amount ? amount : item.MaxStack;
        slot.Amount = curTake;
        return curTake;
    }

    /// <summary>
    /// Seta an item into a slot
    /// </summary>
    /// <param name="slotIndex">The slot number</param>
    /// <param name="itemId">The item id</param>
    /// <param name="amount">The amount</param>
    /// <returns>The amount not set</returns>
    public int SetItem(int slotIndex, int itemId, int amount)
    {

        var item = itemId < 0 ? null : ResourceManager.ItemsById[itemId];
        var slot = _slots[slotIndex];
        
        return SetItem(slot, item, amount);
    }

    /// <summary>
    /// Unsets a slot
    /// </summary>
    /// <param name="slotIndex">The slot index</param>
    public void Unset(int slotIndex)
    {
        var slot = _slots[slotIndex];
        Unset(slot);
    }

    /// <summary>
    /// Unsets a slot
    /// </summary>
    /// <param name="slot">The slot</param>
    public void Unset(InventorySlot slot)
    {
        slot.Id = -1;
        slot.Item = null;
        _freeSlots++;
    }

    /// <summary>
    /// Sets the amount of a slot
    /// </summary>
    /// <param name="slotIndex">The slot index</param>
    /// <param name="amount">The amount</param>
    /// <returns>The amount set</returns>
    public int SetAmount(int slotIndex, int amount)
    {
        var slot = _slots[slotIndex];

        return SetAmount(slot, amount);
    }

    /// <summary>
    /// Sets the amount of a slot
    /// </summary>
    /// <param name="slot">The slot</param>
    /// <param name="amount">The amount</param>
    /// <returns>The amount set</returns>
    public int SetAmount(InventorySlot slot, int amount)
    {
        if (slot.Id < 0)
        {
            return amount;
        }

        var canTake = slot.Item.MaxStack - slot.Amount;
        var curTake = canTake >= amount ? amount : canTake;
        slot.Amount += curTake;
        return curTake;
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds an amount of items to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    public int Add(InventoryItem item, int amount)
    {
        int left = amount;
        int max = item.MaxStack;

        //First look up existing items to fill up amounts
        foreach (var slot in _slots)
        {
            if (slot.Id < 0 || slot.Id != item.Id || slot.Amount >= max)
            {
                continue;
            }

            left -= SetAmount(slot, left);
        }

        //If there is still something left
        //Try to fill in empty slots
        if (left > 0 && _freeSlots > 0)
        {
            foreach (var slot in _slots)
            {
                if (slot.Id >= 0)
                {
                    continue;
                }

                _freeSlots--;

                left -= SetItem(slot, item, left);

                if (_freeSlots <= 0 || left <= 0)
                {
                    break;
                }
            }
        }

        return left;
    }

    /// <summary>
    /// Adds an amount of items to the inventory
    /// </summary>
    /// <param name="itemId">The item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    public int Add(int itemId, int amount)
    {
        InventoryItem item = ResourceManager.ItemsById[itemId];

        return Add(item, amount);
    }

    #endregion

    #region Remove
    
    /// <summary>
    /// Removes the amount of items from inventory
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amopunt not removed</returns>
    public int Remove(InventoryItem item, int amount)
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
        int canTake = 0;
        InventorySlot slot;

        for (int i = 0; i < _slots.Length; i++)
        {
            slot = _slots[i];
            if (slot.Id < 0 || slot.Id != itemId)
            {
                continue;
            }

            canTake = left <= slot.Amount ? left : slot.Amount;
            slot.Amount -= canTake;
            left -= canTake;

            if (slot.Amount <= 0)
            {
                Unset(slot);
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
    /// Searches for the amount of items in the inventory
    /// </summary>
    /// <param name="itemId">The item to search for</param>
    /// <param name="amount">The amount to search for</param>
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
            else if (slot.Id < 0)
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

    public InventorySlot this[int i]
    {
        get
        {
            return _slots[i];
        }
        set
        {
            _slots[i] = value;
        }
    }
    
    #endregion
}