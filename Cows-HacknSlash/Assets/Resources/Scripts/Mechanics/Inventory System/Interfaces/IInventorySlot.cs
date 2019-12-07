using System.ComponentModel;

public interface IInventorySlot : INotifyPropertyChanged
{
    #region Properties

    /// <summary>
    /// The mount if items stacked in the slot
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Is the slot empty
    /// </summary>
    bool Empty { get; }

    /// <summary>
    /// Is the slot full
    /// </summary>
    bool Full { get; }

    /// <summary>
    /// The item id inside the slot
    /// </summary>
    int Id { get; }

    /// <summary>
    /// The index of the slot inside the inventory
    /// </summary>
    int Index { get; set; }

    /// <summary>
    /// The item object
    /// </summary>
    IInventoryItem Item { get; }

    /// <summary>
    /// The type of the slot
    /// </summary>
    SlotType Type { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Adds an amount to the slot stack
    /// </summary>
    /// <param name="amount">The amount to add</param>
    /// <returns>The amount not added</returns>
    int Add(int amount);
    
    /// <summary>
    /// Removes and amount from the slot stack
    /// </summary>
    /// <param name="amount">The amount to remove</param>
    /// <returns>The amount not removed</returns>
    int Remove(int amount);

    /// <summary>
    /// Sets the slot stack to an amount
    /// The amount can't go over the item's max stack
    /// </summary>
    /// <param name="amount">The amount to set</param>
    /// <returns>The amount not set</returns>
    int Set(int amount);

    /// <summary>
    /// Sets the item and the amount inside the slot properly
    /// </summary>
    /// <param name="itemId">The id of the item</param>
    /// <param name="amount">The amount to set</param>
    /// <returns>The amount not set</returns>
    int Set(int itemId, int amount);

    /// <summary>
    /// Swaps the content of the slot with another one
    /// </summary>
    /// <param name="other">The other slot to swap the content with</param>
    void Swap(IInventorySlot other);

    /// <summary>
    /// Empties the slot
    /// </summary>
    void Unset();

    /// <summary>
    /// Check whether the item can be set inside the slot based on item type
    /// </summary>
    /// <param name="itemType">The type of the item</param>
    /// <returns>True if it can, otherwise false</returns>
    bool CanSet(SlotType itemType);

    #endregion
}