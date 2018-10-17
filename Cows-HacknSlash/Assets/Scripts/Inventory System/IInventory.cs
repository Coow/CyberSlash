using System.ComponentModel;

public interface IInventory : INotifyPropertyChanged
{
    #region Properties

    int FreeSlots { get; }
    int Size { get; }

    #endregion

    #region Set

    int Set(int slotIndex, int itemId, int amount);
    int Set(IInventorySlot slot, int itemId, int amount);
    void Unset(int slotIndex);
    void Unset(IInventorySlot slot);

    #endregion

    #region Add

    int Add(int itemId, int amount);
    int Add(IInventoryItem item, int amount);

    #endregion

    #region Remove

    int Remove(int itemId, int amount);
    int Remove(IInventoryItem item, int amount);

    #endregion

    #region Utility

    int GetAmount(int itemId);
    int GetFreeSpace(int itemId);
    bool Contains(int itemId);

    #endregion

    #region Accessors

    IInventorySlot this[int i] { get; set; }
    
    #endregion
}