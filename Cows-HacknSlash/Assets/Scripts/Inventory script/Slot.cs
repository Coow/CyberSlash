using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour {
    // If something is in it.
    public bool somethingin;

    // If the stack is full.
    public bool full;

    // What item the slot is holding.
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
            iteminit = "None";
        }
    }
     
}
