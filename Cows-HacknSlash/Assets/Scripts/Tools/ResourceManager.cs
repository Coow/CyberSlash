using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Used to manage resources
/// </summary>
public static class ResourceManager
{
    //All available Items
    public static InventoryItem[] Items;

    //Dictionary is optimized for search by key
    //Faster than linear lookup for a key in an array
    public static Dictionary<int, InventoryItem> ItemsById;

    public static void Initialize()
    {
        Items = DataManager.DeserializeDataFromFile<InventoryItem[]>(SceneConfiguration.Settings.ItemsListFile);

        ItemsById = new Dictionary<int, InventoryItem>();
        foreach (var item in Items.OrderBy(i => i.Id))
        {
            ItemsById.Add(item.Id, item);
        }
    }
}
