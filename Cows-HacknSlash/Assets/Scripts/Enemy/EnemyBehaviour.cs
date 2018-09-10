using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Transform SwordArmPivot;

    [SerializeField]
    private float ActivateRange;

    private NavMeshAgent _agent;
    private bool _startMoving = false;

    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        var distance = Vector3.Distance(transform.position, Target.position);

        // If player reached a certain distance close to enemy. Enemy would start following player.
        if (distance <= ActivateRange) {
            _startMoving = true;
        }

        if (_startMoving) {
            _agent.SetDestination(Target.position);
        }

        // If enemy reached to stopping distance. Let the enemy start swing the sword.
        if (ReachedToPlayer()) {
            RotateTowardsPlayer();
            SwordArmPivot.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 20), Quaternion.Euler(0, 0, -20), Mathf.PingPong(2 * Time.time, 1f));
        }

	}

    private bool ReachedToPlayer() {
        if (!_agent.pathPending) {
            if (_agent.remainingDistance != 0 && _agent.remainingDistance <= _agent.stoppingDistance) {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }

        return false;
    }

    private void RotateTowardsPlayer() {
        Vector3 directon = (Target.position - transform.position).normalized;
        directon.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directon);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        SwordArmPivot.rotation = Quaternion.LookRotation(directon);
    }
}
