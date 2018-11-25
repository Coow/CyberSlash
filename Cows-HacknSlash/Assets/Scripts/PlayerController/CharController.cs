using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharController : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private Animator anim_Controller;
	private bool m_Running;
	[SerializeField]
	private LayerMask layerMask;
	public float KnockoutTime = 1.5f;
	private bool _knockedOut = false;
	
	[Header("Misc")]
	public GameObject cursorClick;

	public Vector3 cursorOffset;
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
		anim_Controller = gameObject.transform.GetChild(1).GetComponent<Animator>();
	}
	
	// Update is called once per frame.
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Vector3 pointToLook = ray.GetPoint(50f);
		Debug.DrawLine(ray.origin, pointToLook, Color.blue);
		
		if (Input.GetMouseButton(0) & _knockedOut.Equals(false)) {
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				navMeshAgent.destination = hit.point;
				navMeshAgent.updatePosition = true;
				Debug.Log("Player clicked to move");
			}
		}

		if (Input.GetMouseButtonDown(0) & _knockedOut.Equals(false)) {	
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				Vector3 spawnPoint = hit.point + cursorOffset;

				Debug.Log("Player held to move");
				Instantiate(cursorClick, spawnPoint ,Quaternion.identity);
				navMeshAgent.updatePosition = true;	
			}
		}

		// When animations get added.
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			m_Running = false;
			anim_Controller.SetBool("running", m_Running);
			navMeshAgent.updatePosition = false;
		} else {
			m_Running = true;
			anim_Controller.SetBool("running", m_Running);
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
