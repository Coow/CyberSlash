using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Enemy;

public class StateController : MonoBehaviour {

    public State CurrentState;
    public State RemainState; 
    public Transform Eyes;
    public EnemyStats EnemyStats;

    // I don't want that other thing to setup this data, though they are not meant
    // to be setup from inspector, they are declare public cause need to be use in 
    // other class.
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public Enemy.EnemyAttack EnemyAttack;
    [HideInInspector] public List<Transform> WayPointsList;
    [HideInInspector] public int NextWayPoint;
    [HideInInspector] public Transform ChaseTarget;
    [HideInInspector] public float StateTimeElapsed;

    private bool _aiActive;
    private float _tempSphereRadius;

    private void Awake()
    {
        EnemyAttack = GetComponent<Enemy.EnemyAttack>();
        Agent = GetComponent<NavMeshAgent>();
        _tempSphereRadius = EnemyStats.lookSphereCastRadius;
    }

    public void SetUpAI(bool aiActivition, List<Transform> wayPointsList)
    {
        WayPointsList = wayPointsList;
        _aiActive = aiActivition;

        if (_aiActive)
        {
            Agent.enabled = true;
        }
        else
        {
            Agent.enabled = false;
        }
    }

    private void Update()
    {
        // If the AI is not active, return nothing.
        if (!_aiActive)
        {
            return;
        }

        CurrentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (CurrentState != null && Eyes != null)
        {
            Gizmos.color = CurrentState.SceneGizmoColor;
            Gizmos.DrawWireSphere(Eyes.position, EnemyStats.lookSphereCastRadius);
        }
    }

    // Method -> It Check if nextState is not null, Transition to nextState, else stay on current state.
    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        StateTimeElapsed += Time.deltaTime;
        return (StateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        StateTimeElapsed = 0;
    }

    // Method -> to set back Sphere Radius to its intilize state
    private void OnApplicationQuit()
    {
        EnemyStats.lookSphereCastRadius = _tempSphereRadius;
    }
}
