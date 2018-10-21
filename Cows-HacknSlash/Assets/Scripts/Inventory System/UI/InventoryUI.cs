using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Fields

    private IInventory _inventory;

    /// <summary>
    /// The model to use when creating visuals for slots
    /// </summary>
    public GameObject SlotPrefab;

    /// <summary>
    /// The GameObject containing the slots visuals
    /// </summary>
    public GameObject Container;

    /// <summary>
    /// The GameObject displaying information about the inventory
    /// </summary>
    public TextMeshProUGUI Text;

    #endregion

    #region Properties

    /// <summary>
    /// The inventory object
    /// </summary>
    public IInventory Inventory => _inventory;
    
    #endregion
    
    #region Private

    /// <summary>
    /// Reacts to changes with inventory properties
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The event data</param>
    private void Inventory_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateVisuals();
    }

    /// <summary>
    /// Updates the display of the inventory
    /// </summary>
    private void UpdateVisuals()
    {
        Text.text = $"Space left {_inventory.FreeSlots}/{_inventory.Size}";
    }

    #endregion

    /// <summary>
    /// Sets up the display properly based on inventory data
    /// </summary>
    /// <param name="inventory">The inventory to display</param>
    public void Initialize(IInventory inventory)
    {
        if (Container.transform.childCount > 0)
        {
            foreach (Transform child in Container.transform)
            {
                Destroy(child.gameObject);
            }
            Container.transform.DetachChildren();
        }

        if (inventory == null)
        {
            _inventory = null;
            return;
        }

        _inventory = inventory;

        if (inventory == null)
        {
            return;
        }

        inventory.PropertyChanged += Inventory_PropertyChanged;

        for (int i = 0; i < _inventory.Size; i++)
        {
            var go = Instantiate(SlotPrefab, Container.transform);
            go.GetComponent<SlotUI>().Set(_inventory[i]);
        }

        UpdateVisuals();
    }
}
