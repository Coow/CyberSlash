using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField]
	private GameObject HoverText;
	
	void Start () {
		HoverText.SetActive(false);
	}

	public void OnPointerEnter(PointerEventData pointerEventData){
		HoverText.SetActive(true);
	}

	public void OnPointerExit(PointerEventData pointerEventData){
		HoverText.SetActive(false);
	}
}
