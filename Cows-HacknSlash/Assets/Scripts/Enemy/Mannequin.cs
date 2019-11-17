using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DamagableEntity))]
public class Mannequin : MonoBehaviour {
	private NavMeshAgent navMeshAgent;
	private Animator anim_Controller;
	private bool m_Running;

	public GameObject Target;
	public float Range;
	public float AttackRange;
	private bool canStartNewAttack = true;
	[SerializeField]
	[Range(0.0f, 5f)]
	private float animationSpeed = 2.5f;

	[Header("Damage")]
	public GameObject damageObject;
	public int damageToDeal;

	[Header("Sound Effects")]
	public AudioClip aggro;
	public AudioClip damage;
	public AudioClip dying;
	public AudioClip walk;

	private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;


	void Start () {
		anim_Controller = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		anim_Controller.SetBool("running", true);
		
		Target = GameObject.Find("char");

	}
	
	void FixedUpdate(){

		anim_Controller.speed = animationSpeed;

		if(Vector3.Distance(Target.transform.position, transform.position) <= Range)
    	{
    		navMeshAgent.destination = Target.transform.position;
    	} else { 
			if(navMeshAgent.hasPath) {
				//Debug.Log("I should stop moving");
				navMeshAgent.ResetPath();
			}
		}

	
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			m_Running = false;
			anim_Controller.SetBool("walking", m_Running);
			//navMeshAgent.updatePosition = false;
		} else {
			m_Running = true;
			anim_Controller.SetBool("walking", m_Running);
		}

		if(Vector3.Distance(Target.transform.position, transform.position) <= AttackRange) {
			if(canStartNewAttack){StartCoroutine(Attack(1f));}
		}
	}

	private IEnumerator Attack(float stopTime) {

		float attackDelay = 0.91f;

		canStartNewAttack = false;
		anim_Controller.SetTrigger("punch");
		float curSpeed = navMeshAgent.speed;
		navMeshAgent.speed = 0;
		Debug.Log("Attacked", gameObject);

		yield return new WaitForSeconds(attackDelay);

		GameObject go = (GameObject)Instantiate (damageObject, transform.position + (transform.forward.normalized * 2), transform.rotation);
		PlayerDamager _playerDamager = go.GetComponent<PlayerDamager>();
		_playerDamager._DamageToDeal = damageToDeal;

		yield return new WaitForSeconds(stopTime - attackDelay);

		navMeshAgent.speed = curSpeed;
		canStartNewAttack = true;
	}
}
