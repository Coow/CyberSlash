using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour {
    //if something is in it
    public bool somethingin;

    //if the stack is full
    public bool full;

    //what item the slot is holding
    public string iteminit;
    private void FixedUpdate()
    {
        if (transform.childCount != 0)
        {
            somethingin = true;
        }
        if (transform.childCount == 0)
        {
            somethingin = false;
        }
    }
     
}
