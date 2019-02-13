using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpellController : MonoBehaviour
{

    [SerializeField]
    private GameObject SpawnPos;
    public Animator anim_controller;

    [Header("FireBall")]
    [SerializeField]
    private GameObject FireBall;
    public GameObject selectedText;

    [SerializeField]
    private GameObject ice;
    [SerializeField]
    private GameObject poison;

    [Tooltip("0 == null; 1 == FireStaff")]
    public int StaffSelected;
    public float cooldownPeriod = 1f;

    private NavMeshAgent navMeshAgent;

    //Fixing bug being unable too move caused by spamming spells 
    private bool canCastSpell = true;
    public GameObject baseballBat;

    public void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        FireBall.GetComponent<SpellInitialise>().spell.timeStamp = 0;
        ice.GetComponent<SpellInitialise>().spell.timeStamp = 0;
        poison.GetComponent<SpellInitialise>().spell.timeStamp = 0;

        //Temp for testing melee
        //baseballBat = GameObject.Find("BaseballBat");
        baseballBat.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Staff has been selected");
            StaffSelected = 1;
            selectedText.SetActive(true);
            baseballBat.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Staff has been de-selected");
            StaffSelected = 0;
            selectedText.SetActive(false);
            baseballBat.gameObject.SetActive(true);
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
        Debug.Log("Player should stop moving here");
        canCastSpell = false;
        float curSpeed = navMeshAgent.speed;
        navMeshAgent.speed = 0;
        anim_controller.SetTrigger("castSpell");
        
        yield return new WaitForSeconds(stopTime);

        navMeshAgent.speed = curSpeed;
        canCastSpell = true;
        Debug.Log("Player can start moving agian!");
    }
}