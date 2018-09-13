using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyAttack : MonoBehaviour
    {
        public GameObject AttackPrefab;

        private float _nextAttackTime;
        private EnemyType _type;
        private Transform _attackPositoin;
        private Vector3 _target;

        public void Attack(float attackRate)
        {
            if (Time.time > _nextAttackTime)
            {
                _nextAttackTime = Time.time + attackRate;

                if (_type == EnemyType.Magic_Caster)
                {
                    var projectile = Instantiate(AttackPrefab, _attackPositoin.transform.position, Quaternion.identity);
                    _attackPositoin.GetComponent<FireTest>().FollowTarget(_target);
                    _attackPositoin.GetComponent<FireTest>().Shoot(projectile.transform, false);

                    Destroy(projectile.gameObject, 5f);
                }
                else if (_type == EnemyType.Foot_Soldier)
                {
                    // Move the sword (cube) up and down.
                    _attackPositoin.rotation = Quaternion.Lerp(Quaternion.Euler(20, 0, 0),
                        Quaternion.Euler(-20, 0, 0), Mathf.PingPong(5 * Time.time, 1f));
                }
            }
        }
        
        public void SetUpEnemyType(Transform attackPos, EnemyType type)
        {
            _type = type;
            this._attackPositoin = attackPos;
        }

        public void FollowTarget(Vector3 target)
        {
            this._target = target;
        }
    }
}
