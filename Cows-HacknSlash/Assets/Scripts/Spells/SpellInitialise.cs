using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInitialise : MonoBehaviour
{

    public Spell spell;

    // Use this for initialization.
    void Start()
    {

		this.gameObject.GetComponent<MeshRenderer>().material = spell.material;

    }

	void OnTriggerEnter(Collider other){
		// When it collides with something, call the damage the player.
		spell.Damage();
	}

}
