using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour {

    [Header("Starting Stats")]
    public float startingHealth;
    public float startingMana;
    public float startingArmor;
    public float startingMovementSpeed;

    [Header("Current Stats")]
    public int currentLevel = 1;
    public float currentHealth;
    public float currentMAXHealth;
    public float currentMana;
    public float currentMAXMana;
    public float currentArmor;
    public float currentMovementSpeed;

    [Header("Growth Stats")]
    public float growthHealth;
    public float growthMana;
    public float growthArmor;
    public float growthMovementSpeed;

    [Header("MISC")]
    public float invincibilityDuration;
    private float invinEnd = 0f;
    protected NavMeshAgent navAgent;

    [Header("Properties")]
    [SerializeField]
    private float experience;
    public float Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;

            LevelUpCheck();
        }
    }

    [SerializeField]
    private float gold;
    public float Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            gold = Mathf.Clamp(gold, 0, 9999f);
        }
    }

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start ()
    {
        // Setting all of the stats.
        currentHealth = startingHealth;
        currentMAXHealth = startingHealth;

        currentMana = startingMana;
        currentMAXMana = startingMana;

        currentArmor = startingArmor;
        currentMovementSpeed = startingMovementSpeed;
	}
	
	void Update ()
    {
        navAgent.speed = currentMovementSpeed;
	}

    public void LevelUp()
    {
        currentHealth += growthHealth;
        currentMAXHealth += growthHealth;
        currentMana += growthMana;
        currentMAXMana += growthMana;
        currentArmor += growthArmor;
        currentMovementSpeed += growthMovementSpeed;
    }

    public void LevelUpCheck()
    {
        // HACK 100 is a placeholder. 

        if (experience >= 100f)
        {
            LevelUp();
            currentLevel += 1;

            experience -= 100f;
        }
    }

    public void TakeDamage(Attack receivedAttack)
    {
        if (Time.time > invinEnd)
        {
            currentHealth -= receivedAttack.damageValue;
            currentHealth = Mathf.Clamp(currentHealth, 0f, currentMAXHealth);

            if (currentHealth == 0)
            {
                EntityDeath();
            }
            else
            {
                invinEnd = Time.time + invincibilityDuration;
            }
        }
    }

    public void EntityDeath()
    {
        Destroy(this.gameObject);
    }
}
