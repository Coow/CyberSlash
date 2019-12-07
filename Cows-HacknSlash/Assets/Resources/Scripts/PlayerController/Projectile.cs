using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;
    public float projectileSpeed;
    public float DamageToDeal;

    void Start()
    {
        firingPoint = transform.position;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile(){
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit something");
        //Destroy(other.gameObject);
        if(other.GetComponent<DamagableEntity>() != null) {
            other.GetComponent<DamagableEntity>().health -= DamageToDeal;
            Destroy(gameObject);
            Debug.Log("damaged somethings");
        } else {
            Debug.Log("I shouldnt damage this");
        }

        if(other.name == "Flying Virus Updated") {
            Debug.Log("Really th...");
        }
    }

}
