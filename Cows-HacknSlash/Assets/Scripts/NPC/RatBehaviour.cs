using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBehaviour : MonoBehaviour {

   
    [SerializeField]
    private Transform Eyes;             // Spherecast will release ray from this transfrom.

    [Header("Collision")]
    [SerializeField] private float Raduis = 0.5f;      // Radius of Sphere Cast.
    [SerializeField] private float RayLength = 10f;    // Length of Shpere Cast.

    [Header("Rotation Of Eye")]
    [SerializeField]
    private float RotationSpeed = 200f;               

    private NavMeshAgent _agent;
    private RaycastHit _hit;
    
    private Vector3 _playerPosition;

    private int _hitCount = 0;
    private bool _hitPlayer = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        Debug.DrawRay(Eyes.transform.position, Eyes.forward.normalized * RayLength, Color.blue);
        
        RotateEye();
        moveAwayFromPlayer();

        // if Sphere cast hit the player, let the rat move away from player.
        if (Physics.SphereCast(Eyes.transform.position, Raduis, Eyes.forward, out _hit, RayLength)
           && _hit.collider.CompareTag("Player") && _hitCount == 0)
        {
            _hitCount = 1;
            _hitPlayer = true;

            _playerPosition = _hit.collider.transform.position;
        }
    }

    // Method -> Move away rat from player. And let the rat destroy when it reached to specified position.
    private void moveAwayFromPlayer()
    {
        if (_hitPlayer)
        {
            var direction = (_playerPosition - transform.position).normalized;
            _agent.destination = direction;
            
            if (Vector3.Distance(transform.position, direction) <= _agent.stoppingDistance)
            {
                destory();
            }

        }
    }

    // Method -> Rotating eye on y-axis, so it can detech collision from any angle.
    private void RotateEye()
    {
        Eyes.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }

    private void OnDrawGizmos()
    {
        if (Eyes != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Eyes.position, Raduis);
        }
    }

    private void destory()
    {
        Destroy(this.gameObject);
    }
}
