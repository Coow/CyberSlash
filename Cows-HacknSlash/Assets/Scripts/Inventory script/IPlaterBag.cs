public interface IPlaterBag
{
    InventorySlot this[int i] { get; set; }

    int FreeSlots { get; }
    int Size { get; }

    int Add(int itemId, int amount);
    int Add(InventoryItem item, int amount);
    bool Contains(int itemId);
    int GetAmount(int itemId);
    int GetFreeSpace(int itemId);
    int Remove(int itemId, int amount);
    int Remove(InventoryItem item, int amount);
    int SetAmount(int slotIndex, int amount);
    int SetAmount(InventorySlot slot, int amount);
    int SetItem(int slotIndex, int itemId, int amount);
    int SetItem(InventorySlot slot, InventoryItem item, int amount);
}