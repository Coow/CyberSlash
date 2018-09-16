using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        // The amount of health each enemy starts with.
        public float StartingHealth = 100f;
        // How much health the enemy currently has.
        public float _currentHealth;
        // Has the enemy been reduced beyond zero health yet?
        private bool _isDead;
        public TextMesh healthText;                       

        private void Start()
        {
            _currentHealth = StartingHealth;
            _isDead = false;
            healthText.text = "Health: " + _currentHealth.ToString();
        }

        public void TakeDamage(Collider other)
        {
            // Reduce current health by the amount of damage done.
            var spell = other.GetComponent<SpellInitialise>();
            spell.Damage(this);
            healthText.text = "Health: " + _currentHealth.ToString();
            // if current health is at zero or below, call onDeath
            if (_currentHealth <= 0f)
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            Destroy(this.gameObject);
        }

    }

}
