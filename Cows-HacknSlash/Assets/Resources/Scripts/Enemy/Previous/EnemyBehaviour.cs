using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IEnemyController {

    [SerializeField]
    private NavMeshAgent Agent;

    [SerializeField]
    private Transform Target;

    [SerializeField]
    private float ActivateRange;

    [SerializeField]
    private float RotationSpeed = 10f;

    private bool _startMoving = false;

    void Update () {
        var distance = Vector3.Distance(transform.position, Target.position);

        // If player reached a certain distance close to enemy. Enemy would start following player.
        if (distance <= ActivateRange) {
            _startMoving = true;
        }

        if (_startMoving) {
            Agent.SetDestination(Target.position);
        }

        // If enemy reached to stopping distance. Let the enemy Face the player and start Attacking.
        if (ReachedToPlayer()) {
            EnemyAttack();
        }
	}

    // Method to check the remaining distace between player and enenmy and would return true/false on those bases.
    private bool ReachedToPlayer() {
        if (!Agent.pathPending) {
            if (Agent.remainingDistance != 0 && Agent.remainingDistance <= Agent.stoppingDistance) {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }
        return false;
    }

    // Let the Enemy face towards the Player.
    protected void RotateTowardsPlayer() {
        Vector3 directon = (Target.position - transform.position).normalized;
        directon.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directon);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
    }

    public virtual void EnemyAttack() {
        RotateTowardsPlayer();
    }

    public virtual void EnemyHealth() {
        
    }

    public virtual void KillEnemy() {
    }
}
