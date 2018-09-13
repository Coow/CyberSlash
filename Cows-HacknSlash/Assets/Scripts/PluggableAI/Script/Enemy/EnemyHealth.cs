using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {

        public float StartingHealth = 100f;         // The amount of health each enemy starts with.

        private float _currentHealth;               // How much health the enemy currently has.
        private bool _isDead;                       // Has the enemy been reduced beyond zero health yet?

        private void Start()
        {
            _currentHealth = StartingHealth;
            _isDead = false;
        }

        public void TakeDamage(float amount)
        {
            // Reduce current health by the amount of damage done.
            _currentHealth -= amount;

            // if current health is at zero or below, call onDeath
            if (_currentHealth <= 0f)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            Destroy(this.gameObject);
        }

    }

}
