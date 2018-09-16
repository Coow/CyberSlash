using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private bool running;
	[SerializeField]
	private LayerMask layerMask;
	
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
		
		if (Input.GetMouseButton(0)) {
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				navMeshAgent.destination = hit.point;
			}
		}

		if (Input.GetMouseButtonDown(0)) {	
			if(Physics.Raycast(ray, out hit, 100, layerMask)) {
				Vector3 spawnPoint = hit.point + cursorOffset;
				Instantiate(cursorClick, spawnPoint ,Quaternion.identity);
			}
		}

		// When animations get added.
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			running = false;
		} else {
			running = true;
		}
	}
}
