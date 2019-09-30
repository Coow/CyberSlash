using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeController : MonoBehaviour {

	public bool _Enabled = false;
	[Space(2f)]
	[SerializeField]
	public Animator animController;
	private NavMeshAgent navMeshAgent;
	
	private bool canAttack = true;
	public GameObject selectedText;
	[Tooltip("This is before any damage calculations")]
	public int damageToDeal;
	public DamageEnum damageType;

	void Start() {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		if(!_Enabled) {
			return;
		}
		//Change the if statement to be better.. I was tired while coding.. -Cow
		if(Input.GetKeyDown(KeyCode.Q) && canAttack) {
			animController.SetTrigger("baseball");
			StartCoroutine(StopAndAttack(0.75f));
			
		}

		if(Input.GetKeyDown(KeyCode.A) && canAttack) {
			animController.SetTrigger("Slashing");
			StartCoroutine(StopAndAttack(0.75f));
		}
	}

	private IEnumerator StopAndAttack(float stopTime){
		Debug.Log("Player should stop moving here");
		canAttack = false;
		float curSpeed = navMeshAgent.speed;
		navMeshAgent.speed = 0;

		//Should animation be handled here or in the KeyPress?
		yield return new WaitForSeconds(stopTime);

		navMeshAgent.speed = curSpeed;
		canAttack = true;
		Debug.Log("Player can start moving again!");
	}
}
