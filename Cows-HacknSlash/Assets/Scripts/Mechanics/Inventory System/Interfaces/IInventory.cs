using System.ComponentModel;

public interface IInventory : INotifyPropertyChanged
{
    #region Properties

    /// <summary>
    /// The number of free slots in the inventory
    /// </summary>
    int FreeSlots { get; }
    
    /// <summary>
    /// The total number of slots in the inventory
    /// </summary>
    int Size { get; }

    #endregion

    #region Set

    /// <summary>
    /// Seta an item into a slot
    /// </summary>
    /// <param name="slotIndex">The slot number</param>
    /// <param name="itemId">The item id</param>
    /// <param name="amount">The amount</param>
    /// <returns>The amount not set</returns>
    int Set(int slotIndex, int itemId, int amount);

    /// <summary>
    /// Sets an item into a slot
    /// </summary>
    /// <param name="slot">The slot</param>
    /// <param name="item">The item</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not set</returns>
    int Set(IInventorySlot slot, int itemId, int amount);

    /// <summary>
    /// Unsets a slot
    /// </summary>
    /// <param name="slotIndex">The slot index</param>
    void Unset(int slotIndex);

    /// <summary>
    /// Unsets a slot
    /// </summary>
    /// <param name="slot">The slot</param>
    void Unset(IInventorySlot slot);

    #endregion

    #region Add

    /// <summary>
    /// Adds an amount of items to the inventory
    /// </summary>
    /// <param name="itemId">The item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    int Add(int itemId, int amount);

    /// <summary>
    /// Adds an amount of items to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    int Add(IInventoryItem item, int amount);

    #endregion

    #region Remove

    /// <summary>
    /// Removes the amount of items from inventory
    /// </summary>
    /// <param name="itemId">The item to remove</param>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amount not removed</returns>
    int Remove(int itemId, int amount);

    /// <summary>
    /// Removes the amount of items from inventory
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amopunt not removed</returns>
    int Remove(IInventoryItem item, int amount);

    #endregion

    #region Utility
    
    /// <summary>
    /// Searches for the amount of items in the inventory
    /// </summary>
    /// <param name="itemId">The item to search for</param>
    /// <returns>The amount found</returns>
    int GetAmount(int itemId);
    
    /// <summary>
    /// Searches for all available space for the item
    /// </summary>
    /// <param name="itemId">The item</param>
    /// <returns>The amount space available for the item</returns>
    int GetFreeSpace(int itemId);

    /// <summary>
    /// Searches for a presence of items inside the inventory
    /// </summary>
    /// <param name="itemId">The item to search for</param>
    /// <returns>True if item was found, otherwise false</returns>
    bool Contains(int itemId);

    #endregion

    #region Accessors

    /// <summary>
    /// Gets the slot at the index
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The slot at index</returns>
    IInventorySlot this[int index] { get; set; }
    
    #endregion
}