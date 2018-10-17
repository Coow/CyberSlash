using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Fields

    public GameObject SlotPrefab;
    public TextMeshProUGUI Text;
    public GameObject Container;

    public IInventory Inventory;

    #endregion
    
    public void Initialize(IInventory inventory)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in Container.transform)
            {
                Destroy(child.gameObject);
            }
            Container.transform.DetachChildren();
        }

        if (inventory == null)
        {
            Inventory = null;
            return;
        }

        Inventory = inventory;

        if (inventory == null)
        {
            return;
        }

        inventory.PropertyChanged += Inventory_PropertyChanged;
        for (int i = 0; i < Inventory.Size; i++)
        {
            var go = Instantiate(SlotPrefab, Container.transform);
            go.GetComponent<SlotUI>().Set(inventory[i]);
        }

        UpdateVisuals();
    }

    private void Inventory_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        Text.text = $"Space left {Inventory.FreeSlots}/{Inventory.Size}";
    }
}
