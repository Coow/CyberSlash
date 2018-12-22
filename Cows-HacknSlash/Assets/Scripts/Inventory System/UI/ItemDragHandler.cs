using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// The GameObject containg the visual to follow the mouse around
    /// </summary>
    public GameObject VisualItem;

    /// <summary>
    /// This is called when the drag is ongoing
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnDrag(PointerEventData eventData)
    {
        VisualItem.transform.position = Input.mousePosition;// new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
    }

    /// <summary>
    /// This is called when the drag ends
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        VisualItem.transform.localPosition = Vector3.zero;
    }
}
