using System.ComponentModel;

public interface IInventorySlot : INotifyPropertyChanged
{
    #region Properties

    int Amount { get; }
    bool Empty { get; }
    bool Full { get; }
    int Id { get; }
    int Index { get; set; }
    IInventoryItem Item { get; }

    #endregion

    #region Methods

    int Add(int amount);
    int Remove(int amount);
    int Set(int amount);
    int Set(int itemId, int amount);
    void Swap(IInventorySlot other);
    void Unset();
    
    #endregion
}