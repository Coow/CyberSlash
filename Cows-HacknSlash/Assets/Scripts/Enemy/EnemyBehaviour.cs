using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private Transform Target;

    private NavMeshAgent _agent;

    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        _agent.SetDestination(Target.position);
	}
}
