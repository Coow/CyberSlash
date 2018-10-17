using System.ComponentModel;

public interface IInventoryItem : INotifyPropertyChanged
{
    string Description { get; set; }
    int Id { get; set; }
    string Image { get; set; }
    int MaxStack { get; set; }
    string Name { get; set; }
    SlotType Type { get; set; }
}