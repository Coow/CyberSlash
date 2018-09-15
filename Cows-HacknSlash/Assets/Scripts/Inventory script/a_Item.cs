using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class a_Item : MonoBehaviour {

    public Item itemdisplaying;

    public int itemsinit;

    private void Awake() {
        transform.position = transform.parent.position;
    }

    private void FixedUpdate() {
        // Show how many items you have.
        GetComponentInChildren<TextMeshProUGUI>().text = itemsinit.ToString();

        // Set position to the parent.
        if (itemdisplaying != null) {
            gameObject.GetComponent<Image>().sprite = itemdisplaying.sprite;         
        }
    }
}
