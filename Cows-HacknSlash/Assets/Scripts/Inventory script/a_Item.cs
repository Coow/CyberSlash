using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class a_Item : MonoBehaviour {

    public Item itemdisplaying;

    public int itemsinit;

    public bool mouseisover;
    public bool inhand;

    private void Awake()
    {
        transform.position = transform.parent.position;
    }


    //a bool thats true when mouse is over and false when its not
    public void mouseenter()
    {
        mouseisover = true;
    }

    public void mouseexit()
    {
        mouseisover = false;
    }




    private void Update()
    {
        //if the mouse is over and the mouse button is pressed tell the script you are holding the object
        if (mouseisover && Input.GetMouseButtonDown(0))
        {
            inhand = true;
        }
        //if the mouse button goes up go too the slot it has just touched
        if (Input.GetMouseButtonUp(0))
        {
            inhand = false;
            transform.position = transform.parent.position;
            GetComponentInParent<Slot>().somethingin = true;
            GetComponentInParent<Slot>().iteminit = itemdisplaying.Name;
            
            //if the item is full make the slots info too full
            if(itemsinit == itemdisplaying.HowManyCanStack)
            {
                GetComponentInParent<Slot>().full = true;
            }
        }
    }


    private void FixedUpdate() {
        // Show how many items you have.
        GetComponentInChildren<TextMeshProUGUI>().text = itemsinit.ToString();

        // Set position to the parent.
        if (itemdisplaying != null) {

            gameObject.GetComponent<Image>().sprite = itemdisplaying.sprite;         
        }

        //if you are holding in the object follow the mouse
        if (inhand)
        {
            transform.position = new Vector2((Input.mousePosition.x), Input.mousePosition.y);
        }
    }

    //when you are holding the item and touch another slot make that slot the parent
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item_Slot" && !collision.gameObject.GetComponent<Slot>().somethingin)
        {
            GetComponentInParent<Slot>().somethingin = false;
            GetComponentInParent<Slot>().full = false;
            transform.SetParent(collision.gameObject.transform);
        }
    }
}
