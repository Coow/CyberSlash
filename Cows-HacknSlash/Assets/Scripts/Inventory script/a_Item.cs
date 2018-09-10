using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class a_Item : MonoBehaviour {

    public Item itemdisplaying;

    public int itemsinit;

    private void FixedUpdate()
    {
        //show how many items u have
        GetComponentInChildren<TextMeshProUGUI>().text = itemsinit.ToString();

        //set position too the parent
        transform.position = transform.parent.position;

        if (itemdisplaying != null)
        {
            gameObject.GetComponent<Image>().sprite = itemdisplaying.sprite;         
        }
    }
}
