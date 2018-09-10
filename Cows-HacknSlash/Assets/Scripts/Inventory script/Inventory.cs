using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour {

    // Item array with all items able to go into the inventory
    public Item[] Alltheitems;

    //the actual item in the inventory, later being set too the item of choice
    public GameObject itemdisplayerprefab;

    public GameObject[] Slots;

    public GameObject Backpack;

    private void Awake()
    {
        Backpack.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Additem(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Additem(1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(Backpack.activeInHierarchy)
            {
                Backpack.SetActive(false);
            } else
            {
                Backpack.SetActive(true);
            }
        }

    }
    // being able too call this function and just typing in what item id too add
    public void Additem(int id , int amount)
    {
        foreach(GameObject slot in Slots)
        {
            //if nothing is in the slot
            if (!slot.GetComponent<Slot>().somethingin)
            {
                //spawn the object in that slot+
                Instantiate(itemdisplayerprefab, slot.transform.localPosition, Quaternion.identity, slot.gameObject.transform);

                //making sure the slot is displayed as something in it
                slot.GetComponent<Slot>().somethingin = true;
                slot.GetComponentInChildren<a_Item>().itemdisplaying = Alltheitems[id];
                slot.GetComponentInChildren<a_Item>().itemsinit += amount;
                slot.GetComponent<Slot>().iteminit = Alltheitems[id].Name;
                break;
            }
            //if there is something in the slot and its the same as the one u are trying too add
            else if (slot.GetComponent<Slot>().iteminit == Alltheitems[id].Name && !slot.GetComponent<Slot>().full)
            {
                slot.GetComponentInChildren<a_Item>().itemsinit += amount;

                //if it is then full after u have added the amount of items
                if(slot.GetComponentInChildren<a_Item>().itemsinit >= Alltheitems[id].HowManyCanStack)
                {
                    slot.GetComponent<Slot>().full = true;
                }
                break;
            }
        }
      
    }
}
