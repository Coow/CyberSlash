using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Additem : MonoBehaviour {

    public Inventory inventory;

    public int ObjectID;

    public int Objectamount = 1;
	void Start () {
        inventory = GameObject.Find("PlayerCharacter/char").GetComponent<Inventory>();
	}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            inventory.Additem(ObjectID, Objectamount);
            Destroy(gameObject);
        }
    }
}
