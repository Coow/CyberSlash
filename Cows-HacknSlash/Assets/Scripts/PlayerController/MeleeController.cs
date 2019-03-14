using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeController : MonoBehaviour {

	[SerializeField]
	public Animator animController;
	private NavMeshAgent navMeshAgent;
	
	private bool canAttack = true;
	public GameObject selectedText;

	void Start() {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void FixedUpdate() {
		
	}

	void Update () {
		//Change the if statement to be better.. I was tired while coding.. -Cow
		if(Input.GetKeyDown(KeyCode.Q) && canAttack) {
			if(selectedText.gameObject.active == true) {
				Debug.Log("Cannot melee");
				return;
			}
			else {
				animController.SetTrigger("baseball");
				StartCoroutine(StopAndAttack(0.75f));
			}
		}

		if(Input.GetKeyDown(KeyCode.W) && canAttack) {
			if(selectedText.gameObject.active == true) {
				Debug.Log("Cannot melee");
				return;
			}
			else {
				animController.SetTrigger("Slashing");
				StartCoroutine(StopAndAttack(0.75f));
			}
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
