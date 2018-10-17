using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject VisualItem;

    public void OnDrag(PointerEventData eventData)
    {
        VisualItem.transform.position = Input.mousePosition;// new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        VisualItem.transform.localPosition = Vector3.zero;
    }
}
