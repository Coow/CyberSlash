using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{   
  
    public int _DamageToDeal;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter(Collision other){
        Debug.Log("I hit something!");
        if(other.gameObject.tag == "Player") {

            var _CharController = other.gameObject.GetComponent<CharController>();
            _CharController.curHealth -= _DamageToDeal;
            Destroy(gameObject);
        }
    }
}
