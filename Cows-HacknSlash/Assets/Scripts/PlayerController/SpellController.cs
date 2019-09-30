using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpellController : MonoBehaviour
{
    public bool _Enabled = false;
    [Space(2f)]
    [SerializeField]
    private GameObject SpawnPos;
    public Animator anim_controller;
    public GameObject selectedText;
    
    [SerializeField]
    private GameObject FireBall, ice, poison;

    [Tooltip("0 == null; 1 == FireStaff;" )]
    public int StaffSelected;

    public int damageToDeal;
	public DamageEnum damageType;

    //Used for StopAndCast function
    private NavMeshAgent navMeshAgent;

    [Space(1f)]
    private bool canCastSpell = true;

    public void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        FireBall.GetComponent<SpellInitialise>().spell.timeStamp = 0;
        ice.GetComponent<SpellInitialise>().spell.timeStamp = 0;
        poison.GetComponent<SpellInitialise>().spell.timeStamp = 0;
    }

    void Update()
    {
        if(!_Enabled) {
			return;
		}
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Staff has been selected");
            StaffSelected = 1;
            selectedText.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Melee has been selected");
            StaffSelected = 0;
            selectedText.SetActive(false);
        }

        if (StaffSelected == 1 && canCastSpell)
        {
            if (Input.GetKeyDown(KeyCode.E) && FireBall.GetComponent<SpellInitialise>().spell.timeStamp <= Time.time)
            {
                FireBall.GetComponent<SpellInitialise>().spell.timeStamp = Time.time + FireBall.GetComponent<SpellInitialise>().spell.cooldownPeriod;
                var projectile = Instantiate(FireBall, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
                StartCoroutine(StopAndCast(0.75f));
                
            }
            else if (Input.GetKeyDown(KeyCode.R) && ice.GetComponent<SpellInitialise>().spell.timeStamp <= Time.time)
            {
                ice.GetComponent<SpellInitialise>().spell.timeStamp = Time.time + ice.GetComponent<SpellInitialise>().spell.cooldownPeriod;
                var projectile = Instantiate(ice, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
                StartCoroutine(StopAndCast(0.75f));
            }
            else if (Input.GetKeyDown(KeyCode.T) && poison.GetComponent<SpellInitialise>().spell.timeStamp <= Time.time)
            {
                poison.GetComponent<SpellInitialise>().spell.timeStamp = Time.time + poison.GetComponent<SpellInitialise>().spell.cooldownPeriod;
                var projectile = Instantiate(poison, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
                StartCoroutine(StopAndCast(0.75f));
            }
        }        
    }

    private IEnumerator StopAndCast(float stopTime){
        //Debug.Log("Player should stop moving here");
        canCastSpell = false;
        float curSpeed = navMeshAgent.speed;
        navMeshAgent.speed = 0;
        anim_controller.SetTrigger("castSpell");
        
        yield return new WaitForSeconds(stopTime);

        navMeshAgent.speed = curSpeed;
        canCastSpell = true;
        //Debug.Log("Player can start moving agian!");
    }

    public void CalculateAttackDamage(string _AttackLevel) {
        
    }
}