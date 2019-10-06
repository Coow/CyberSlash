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

    void OnTriggerEnter(Collider collision) {
        Debug.Log("Something triggered me", gameObject);
        if(collision.gameObject.tag == "SpellBall"){
            TakeDamage(collision.gameObject.GetComponent<SpellInitialise>().spell.damage);
        }
    }

    public void TakeDamage (float amount) {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0){
            Destroy(gameObject);
        }
    }

}
