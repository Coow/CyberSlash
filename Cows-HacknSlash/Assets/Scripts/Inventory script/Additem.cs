using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Additem : MonoBehaviour {

    public Inventory invenscript;

    public int ObjectID;

    public int Objectamount = 1;
	void Start () {
        invenscript = GameObject.Find("PlayerCharacter/char").GetComponent<Inventory>();
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            invenscript.Additem(ObjectID, Objectamount);
            Destroy(gameObject);
        }
    }
}
