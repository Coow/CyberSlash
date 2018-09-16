using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell")]
public class Spell : ScriptableObject
{

    public new string name;
    public Material material;
    [Tooltip("Total per tick")]
    public int damage;
    [Header("Ticking Damage")]
    public float initialDamage;
    public bool isTickingDamage;
    public float duration = 3f;

    public void Damage(Enemy.EnemyHealth enemy)
    {
        if (enemy != null)
        {
            // Function to run when dealing damage to enemies.
            if (!isTickingDamage)
            {
                enemy._currentHealth -= damage;
            }
            else
            {
                enemy._currentHealth -= initialDamage;
            }
            if (enemy._currentHealth <= 0f)
            {
                enemy.OnDeath();
            }
            enemy.healthText.text = "Health: " + enemy._currentHealth.ToString();
            Debug.Log("Enemy Health: " + enemy._currentHealth);
        }
    }

    public IEnumerator DoTickingDamage(Enemy.EnemyHealth enemy)
    {
        if (enemy != null)
        {
            float length = duration;
            while (length > 0)
            {
                if (enemy._currentHealth <= 0f)
                {
                    enemy.OnDeath();
                }
                enemy._currentHealth -= damage;
                Debug.Log("Enemy Health: " + enemy._currentHealth);
                length -= 1;
                enemy.healthText.text = "Health: " + enemy._currentHealth.ToString();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
