using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagableEntity : MonoBehaviour
{   
    public float health;
    private float startHealth;


    public Image healthBar;

    void Start(){
        startHealth = health;
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Something triggered me", gameObject);

    }

    public void TakeDamage (float amount) {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0){
            Destroy(gameObject);
        }
    }

}
