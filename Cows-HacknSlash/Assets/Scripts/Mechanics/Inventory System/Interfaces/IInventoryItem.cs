using System.ComponentModel;

public interface IInventoryItem : INotifyPropertyChanged
{
    /// <summary>
    /// The description of the item
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// The id of the item
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// The image name and path
    /// </summary>
    string Image { get; set; }

    /// <summary>
    /// How much of the item can be piled in a single inventory slot
    /// </summary>
    int MaxStack { get; set; }

    /// <summary>
    /// The item's name
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// The type of slot the item can go in
    /// </summary>
    SlotType Type { get; set; }
}