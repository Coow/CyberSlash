using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell")]
public class Spell : ScriptableObject
{

	public new string name;
    public Material material;
    public int damage;

    public void Damage()
    {
        // Function to run when dealing damage to enemies.
        Debug.Log("Damage dealt: " + damage);
    }

}
