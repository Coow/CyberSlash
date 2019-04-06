using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingDragon : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private Animator anim_Controller;
	private bool m_Running;
	
	[SerializeField]
	private GameObject Target;
	public float Range;
	[SerializeField]
	[Range(0.0f, 5f)]
	private float animationSpeed = 2.5f;

	[Header("Sound Effects")]
	public AudioClip aggro;
	public AudioClip damage;
	public AudioClip dying;
	public AudioClip walk;

	private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;


	void Start () {
		anim_Controller = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		anim_Controller.SetBool("running", true);
		
		source = GetComponent<AudioSource>();
	}
	
	void FixedUpdate(){

		anim_Controller.speed = animationSpeed;

		

		if(Target.gameObject != null) {
			if(Vector3.Distance(Target.transform.position, transform.position) <= Range)
			{
				navMeshAgent.destination = Target.transform.position;
			}
		} else {
			Target = GameObject.Find("char");
		}

		
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			m_Running = false;
			anim_Controller.SetBool("running", m_Running);
			//navMeshAgent.updatePosition = false;
		} else {
			m_Running = true;
			anim_Controller.SetBool("running", m_Running);
		}
	}

	void PlayWalkSound(){
        source.pitch = Random.Range (lowPitchRange,highPitchRange);

		source.PlayOneShot(damage,0.5f);
	}
}
