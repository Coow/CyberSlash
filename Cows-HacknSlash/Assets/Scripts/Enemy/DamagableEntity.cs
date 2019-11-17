using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEntity : MonoBehaviour
{   
    public float health;
    private float startHealth;

    [HideInInspector] public float maxHealth;

    public GameObject HealthBar;
    private EnemyHealthBar enemyHealthBar;

    void Start(){
        startHealth = health;
        maxHealth = health;

        enemyHealthBar = HealthBar.GetComponent<EnemyHealthBar>();
        enemyHealthBar.meshRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider collision) {
        Debug.Log("Something triggered me", gameObject);
        if(collision.gameObject.tag == "SpellBall"){
            TakeDamage(collision.gameObject.GetComponent<SpellInitialise>().spell.damage);
        }
    }

    public void TakeDamage (float amount) {
        if(!enemyHealthBar.meshRenderer.enabled){
            enemyHealthBar.meshRenderer.enabled = true;
        }
        health -= amount;

        enemyHealthBar.UpdateFill(health / (float)maxHealth);

        if(health <= 0){
            Debug.Log(gameObject.name + " died!");
            Destroy(gameObject);
        }
    }

}
