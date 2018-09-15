using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInitialise : MonoBehaviour
{

    public Spell spell;
    private TrailRenderer trail;

    // Use this for initialization.
    void Start()
    {

        this.gameObject.GetComponent<MeshRenderer>().material = spell.material;
        trail = this.gameObject.GetComponent<TrailRenderer>();
        trail.material = spell.material;

    }

    void OnTriggerEnter(Collider other)
    {
        // When it collides with something, call the damage the player.
        spell.Damage();
    }

}
