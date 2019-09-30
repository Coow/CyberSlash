using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class CharController : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private Animator anim_Controller;
	private bool m_Running;
	[SerializeField]
	[Tooltip("Layers the RayCasts will check if it has hit\nApplies to both PortalHop and ClickToMove")]
	private LayerMask layerMask;
	public float KnockoutTime = 1.5f;
	private bool _knockedOut = false;
	private bool canMove = true;
	public bool dead = false;
	
	[Header("Misc")]
	public GameObject cursorClick;
	public Vector3 cursorOffset;
	private CapsuleCollider capscol;


	[SerializeField]
	private float PortalDistance = 5f;
	[SerializeField]
	private GameObject portalEndPoint;

	[Header("Inventory Related")]
	[SerializeField]
	private Animator inventoryAnimator;
	[SerializeField]
	private bool levelsShown = false;


	[Header("Health and Resources")]
	private int startingHealth = 10;
	[HideInInspector]
	public string strHealth;
	public int curHealth;
	public int maxHealth;
	public TMP_Text HealthText;

	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
		anim_Controller = gameObject.transform.GetChild(1).GetComponent<Animator>();
		capscol = GetComponent<CapsuleCollider>();
		capscol.enabled = true;
		curHealth = startingHealth;
		maxHealth = startingHealth;
	}
	
	// Update is called once per frame.
	void Update () {
		playerClickMove();

		//Portal hop
		if (Input.GetKeyDown(KeyCode.Space)){
			anim_Controller.SetTrigger("portalling");
			//anim_Controller.SetBool("running", m_Running);
			navMeshAgent.ResetPath();
			StartCoroutine(PortalHop(0.2f));

		}

		// Running
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			m_Running = false;
			anim_Controller.SetBool("running", m_Running);
			navMeshAgent.updatePosition = false;
		} 
		else {
			m_Running = true;
			anim_Controller.SetBool("running", m_Running);
		}
	
		if (Input.GetKeyDown(KeyCode.Tab)) {
			Debug.Log("Tab pressed");
			if(levelsShown == false) {
				inventoryAnimator.SetBool("showLevels", true);
				levelsShown = true;
			} else {
				inventoryAnimator.SetBool("showLevels", false);
				levelsShown = false;
			}
			inventoryAnimator.SetTrigger("levelsTrigger");
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

	public void Die(){
		Debug.Log("DIE");
		_knockedOut = true;
		if(dead == false) {
			anim_Controller.SetBool("dead", true);
			dead = true;
			capscol.enabled = false;
		}
		
	}

	private IEnumerator PortalHop(float waitTime){
		Debug.Log("Going to warp!");

		yield return new WaitForSeconds(waitTime);
		//Jump to the Portal GameObject
		navMeshAgent.Warp(portalEndPoint.transform.position);

		Debug.Log("Warped!");
	}

	void FixedUpdate() {
		#region PortalObjectCalculations
		Vector3 forward = transform.TransformDirection(Vector3.forward) * PortalDistance;
		Debug.DrawRay(transform.position, forward, Color.green);
		
		RaycastHit hit;

		if(Physics.Raycast(transform.position, transform.forward, out hit, PortalDistance, layerMask)) {
			if(hit.collider.name==null) {
				portalEndPoint.transform.position = transform.position + forward.normalized * PortalDistance;
				return;
			} else {
				portalEndPoint.transform.position = hit.point - forward.normalized;
			}
		}
		#endregion

		#region HealthCalc

		if(curHealth > maxHealth) {
			curHealth = maxHealth;
		}

		strHealth = curHealth + " / " + maxHealth;

		HealthText.text = $"{strHealth}";

		if(curHealth <= 0) {
			Die();
		}
		#endregion
	}

	public void CalculatePlayerSpeed(string _AgilityLevel){
		navMeshAgent.speed = 8 + (ulong.Parse(_AgilityLevel) / 10);
	}

	public void CalculatePlayerMaxHealth(string _DefenseLevel){
		maxHealth = 8 + (int.Parse(_DefenseLevel) * 2); 
	}

	private void playerClickMove(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Vector3 pointToLook = ray.GetPoint(50f);
		Debug.DrawLine(ray.origin, pointToLook, Color.blue);
		
		if (Input.GetMouseButton(1) & _knockedOut.Equals(false)) {
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				navMeshAgent.destination = hit.point;
				navMeshAgent.updatePosition = true;
			}
		}

		if (Input.GetMouseButtonDown(1) & _knockedOut.Equals(false)) {	
			if (Physics.Raycast(ray, out hit, 100, layerMask)) {
				Vector3 spawnPoint = hit.point + cursorOffset;

				//Debug.Log("Player held to move");
				Instantiate(cursorClick, spawnPoint ,Quaternion.identity);
				navMeshAgent.updatePosition = true;	
			}
		}	
	}


}