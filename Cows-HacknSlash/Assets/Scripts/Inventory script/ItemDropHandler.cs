using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //This should always be a slot
        var drag = eventData.pointerDrag.GetComponent<SlotUI>();

        MonoBehaviour drop = eventData.pointerEnter.GetComponent<ItemUI>();

        //If we got something we're dropping onto an item
        if (drop != null)
        {
            drop = drop.transform.parent.GetComponent<SlotUI>();
        }
        else
        {
            drop = eventData.pointerEnter.GetComponent<SlotUI>();
        }

        //If we got something, we can do checks to add or swap
        if (drop != null)
        {
            //Handle exchange
            //2 Cases :
            //#1 - Item id is same, so add
            //#2 - Item id is different, so swap
            SlotUI slot = drop as SlotUI;

            if (drag.Slot.Id == slot.Slot.Id)
            {
                drag.Slot.Amount = slot.Inventory.Inventory.Add(drag.Slot.Id, drag.Slot.Amount);
            }
            else
            {
                var item = drag.Slot.Item;
                var amount = drag.Slot.Amount;
     
                if (slot.Slot.Id < 0)
                {
                    drag.Inventory.Inventory.Unset(drag.Slot);
                }
                else
                {
                    drag.Inventory.Inventory.SetItem(drag.Slot, slot.Slot.Item, slot.Slot.Amount);
                }

                if (item == null)
                {
                    slot.Inventory.Inventory.Unset(slot.Slot);
                }
                else
                {
                    slot.Inventory.Inventory.SetItem(slot.Slot, item, amount);
                }
            }

            drop = null;
            //drop = drop.transform.parent.GetComponent<InventoryUI>();
        }
        else
        {
            drop = eventData.pointerEnter.GetComponent<InventoryUI>();
        }

        //If we got something, we can do a simple add
        if (drop != null)
        {
            InventoryUI inv = drop as InventoryUI;

            inv.Inventory.Add(drag.Slot.Item, drag.Slot.Amount);
        }

        Debug.Log($"Error : Drop detected but couldn't determine drop type");
        Debug.Log($"Drag({drag})");
        Debug.Log($"Drop({drop})");
    }
}
