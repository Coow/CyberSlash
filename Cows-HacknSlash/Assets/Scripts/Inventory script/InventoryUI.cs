using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject SlotPrefab;

    //For debug purposes
    private PlayerBag _bag;

    //The actual inventory
    public IInventory Inventory;

    
    public void Initialize(IInventory inventory)
    {
        if (inventory == null)
        {
            Inventory = null;
            return;
        }
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            transform.DetachChildren();
        }

        InventorySlot slot;
        SlotUI ui;
        Inventory = inventory;
        _bag = inventory as PlayerBag;
        for (int i = 0; i < Inventory.Size; i++)
        {
            var go = Instantiate(SlotPrefab, transform);
            slot = inventory[i];

            if (slot.Id < 0)
            {
                continue;
            }

            go.GetComponent<SlotUI>().Set(slot);
        }
    }
}
