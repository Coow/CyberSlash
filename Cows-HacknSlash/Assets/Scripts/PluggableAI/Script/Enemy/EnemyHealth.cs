using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        // The amount of health each enemy starts with.
        public float StartingHealth = 100f;
        // How much health the enemy currently has.
        private float _currentHealth;
        // Has the enemy been reduced beyond zero health yet?
        private bool _isDead;                       

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
