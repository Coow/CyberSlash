using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowBossController : MonoBehaviour {

	[Header("Gameplay")]
	[Tooltip("How many ticks per minute, or how many actions can be done in a minute.")]
	public float TickPerMin = 10;
	private float TickRate;
	private float curTick = 0;
	public float BossHealth;
	public GameObject CowMinion;

	[Header("Targeting")]
	private Transform target;
	[SerializeField]
	private float MinDistance;

	private NavMeshAgent navMeshAgent;
	private Animator animator;

	[Header("Shake")]
	private GameObject shakeDamageObject;
	private Animator shakeAnimator;

	// Failsafe, to make sure the shake object does go to shit.
	private bool ShakeEnabled = true;

	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		
		shakeDamageObject = this.gameObject.transform.GetChild(0).gameObject;
		

		shakeDamageObject.SetActive(false);

		if (!shakeDamageObject.name.Equals("ShakeHit")){
			Debug.LogError("ShakeHitObject has not been set properly. Make sure the ShakeHit Object is the 1st child of the CowBoss");
			ShakeEnabled = false;
		} else {
			// Set the animator of it!
			shakeAnimator = shakeDamageObject.GetComponent<Animator>();
		}

		// Calculate the tickrate.
		TickRate = 60 / TickPerMin;
		Debug.Log("Tick rate has been set too: " + TickRate);

		// Just to be sure.
		target = null;
		SetTarget();

		Movement();


	}
	
	void FixedUpdate () {
		// Look at the target at all times!
		//transform.LookAt(target);

		//Movement();
	}


	void Update(){
		// TEST
		if (Input.GetKeyDown(KeyCode.S)){
			Shake();
		}
	}
	
	// Randomly select a target.
	public void SetTarget()
	{
		// Once multiplayer is properly implemented, add a fuction to randomly select a player, or a system to determine the target.
		// For now, it will just find the player in the scene, and target dat lonely boi.

		target = GameObject.FindWithTag("Player").transform;
		Debug.Log("Target has been set too:" + target.name);
	}

	// Do the stomp!
	public void Stomp()
	{


	}

	// Rawr xD
	public void Growl()
	{

	}

	// Do the harlem shake!
	public void Shake()
	{
		// Fail safe
		if (ShakeEnabled == false){
			shakeDamageObject.SetActive(false);
			return;
		}
		shakeDamageObject.SetActive(true);
		shakeAnimator.SetTrigger("SHAKE");



		// Its kinda broken.. Enable the COWBOSS animator at your own risk!
		// animator.SetTrigger("Shake");

	}

    public void AlertObservers(string message)
    {
		// Needs to be set in the animation, will fix eventually
        if (message.Equals("AttackAnimationEnded"))
        {
			shakeDamageObject.SetActive(false);
        }
    }

	// Spawn minions!
	public void CowMinions()
	{

	}

	// MOOOOOOOOVE
	public void Charge()
	{

	}

	// Move you fat cow!
	public void Movement()
	{
		// Moves towards the target player!
		navMeshAgent.destination = target.position;
	}

	// Spell hit detection
	private void OnTriggerEnter(Collider other)
	{
		// This is probably the most retarded code ive written, so someone please rewrite it to get the damage from the SpellSO.. Thanks...
		if (other.tag.Equals("SpellBall"))
		{
			//_Damage = other.GetComponent<SpellInitialise>().spell.damage;
			if(other.name.Equals("FireBall(Clone)"))
			{
				BossHealth -= 5;
			}
			else if(other.name.Equals("Ice(Clone)"))
			{
				BossHealth -= 30;
			}
			else if(other.name.Equals("Poison(Clone)"))
			{
				BossHealth -= 15;
			}
			print("Get Hit");
			//print(BossHealth);
		}
		else 
		{
			Debug.Log("I dont know what I got hit by!!");
		}
	}	
}