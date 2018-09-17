using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public enum EnemyType { Foot_Soldier, Magic_Caster };

    public class EnemyManager : MonoBehaviour
    {
        public List<Transform> WayPointsForAI;

        public EnemyType EnemyTypeChoice;

        public Transform AttackPosition;

        private StateController _stateController;
        private EnemyAttack _enemyAttack;
        private EnemyHealth _enemyHealth;

        private void Start() {
            _stateController = GetComponent<StateController>();
            _stateController.SetUpAI(true, WayPointsForAI);

            _enemyAttack = GetComponent<EnemyAttack>();
            _enemyAttack.SetUpEnemyType(AttackPosition, EnemyTypeChoice);

            _enemyHealth = GetComponent<EnemyHealth>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Fire_Ball"))
            {
                _enemyHealth.TakeDamage(other);
                print("Get Hit");
            }
        }
    }
}
