using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharController : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private bool running;
	[SerializeField]
	private LayerMask layerMask;
	public float KnockoutTime = 1.5f;
	private bool _knockedOut = false;
	
	[Header("Misc")]
	public GameObject cursorClick;

	public Vector3 cursorOffset;
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame.
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Vector3 pointToLook = ray.GetPoint(50f);
		Debug.DrawLine(ray.origin, pointToLook, Color.blue);
		
		if (Input.GetMouseButton(0) & ) {
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				navMeshAgent.destination = hit.point;
				navMeshAgent.updatePosition = true;
			}
		}

		if (Input.GetMouseButtonDown(0)) {	
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				Vector3 spawnPoint = hit.point + cursorOffset;

				Instantiate(cursorClick, spawnPoint ,Quaternion.identity);
				navMeshAgent.updatePosition = true;	
			}
		}

		// When animations get added.
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			running = false;
			navMeshAgent.updatePosition = false;
		} else {
			running = true;
		}
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Fire_Ball_Enemy")) {
            this.gameObject.SetActive(false);
        }

        if (other.tag.Equals("Enemy_Sword")) {
            this.gameObject.SetActive(false);
        }
    }

    private void DestroyPlayer() {
        Destroy(this.gameObject);
    }

	public void KnockedOut(){

	}
}
