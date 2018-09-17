using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public float damageValue;
    public string damageType;
    
    public Attack(float damageValue, string damageType)
    {
        this.damageValue = damageValue;
        this.damageType = damageType;
    }

    // Copy Constructor:
    public Attack (Attack attackToCopy)
    {
        attackToCopy.damageValue = damageValue;
        attackToCopy.damageType = damageType;
    }
}
