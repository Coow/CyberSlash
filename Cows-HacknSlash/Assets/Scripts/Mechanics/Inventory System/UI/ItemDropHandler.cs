using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// This is called when the drag ends and a drop point is detected
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnDrop(PointerEventData eventData)
    {
        //Check if the draged object wasn't droped onto itself
        Debug.Log("Drop detected");
        if (ReferenceEquals(eventData.pointerDrag, eventData.pointerEnter))
        {
            Debug.Log("Same object");
            return;
        }

        //This should always be a slot
        var drag = eventData.pointerDrag.GetComponent<SlotUI>();

        //Check if the drop happened on an item
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
                drag.Slot.Set(slot.Slot.Add(drag.Slot.Amount));
            }
            else
            {
                Debug.Log("Swapping");
                slot.Slot.Swap(drag.Slot);
            }
            return;
        }
        else
        {
            Debug.Log("Getting the inventory");
            drop = eventData.pointerEnter.GetComponent<InventoryUI>();
        }

        //If we got something, we can do a simple add
        if (drop != null)
        {
            Debug.Log("Moving to inventory");
            InventoryUI inv = drop as InventoryUI;

            drag.Slot.Set(inv.Inventory.Add(drag.Slot.Item, drag.Slot.Amount));
        }
    }
}
