using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{   
    [HideInInspector]
    public int _DamageToDeal;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void OnCollisonEnter(Collision other){
        Debug.Log("I hit something!");
        Debug.Log(other);
        if(other.gameObject.name == "char") {
            var _CharController = other.gameObject.GetComponent<CharController>();
            _CharController.curHealth -= _DamageToDeal;
        }
    }
}
