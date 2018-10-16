public interface IInventory
{
    #region Properties

    int FreeSlots { get; }
    int Size { get; }

    #endregion

    #region Set

    int SetAmount(int slotIndex, int amount);
    int SetAmount(InventorySlot slot, int amount);
    int SetItem(int slotIndex, int itemId, int amount);
    int SetItem(InventorySlot slot, InventoryItem item, int amount);
    void Unset(int slotIndex);
    void Unset(InventorySlot slot);

    #endregion

    #region Add

    int Add(int itemId, int amount);
    int Add(InventoryItem item, int amount);

    #endregion

    #region Remove

    int Remove(int itemId, int amount);
    int Remove(InventoryItem item, int amount);

    #endregion

    #region Utility

    int GetAmount(int itemId);
    int GetFreeSpace(int itemId);
    bool Contains(int itemId);

    #endregion

    #region Accessors

    InventorySlot this[int i] { get; set; }
    
    #endregion
}