using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTester : MonoBehaviour
{

    void FixedUpdate()
    {
            
    }

    void OnCollision(){
        Debug.Log("I hit something!", gameObject);
        /*if(other.gameObject.tag == "Player") {
            var _CharController = other.gameObject.GetComponent<CharController>();
            _CharController.curHealth -= _DamageToDeal;
        }*/
    }
}
