using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	[SerializeField]
	private GameObject SpawnPos;

	[Header("FireBall")]

	public GameObject fireball;
  public float FireBallSpeed;
	private float distance = 10f;

	void Update () {
		
		if(Input.GetKeyDown(KeyCode.E)){	
     
        	var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        	position = Camera.main.ScreenToWorldPoint(position);
        	var spell = Instantiate(fireball, transform.position, Quaternion.identity) as GameObject;
        	spell.transform.LookAt(position);    
        	//spell.GetComponent<Rigidbody>().AddForce(spell.transform.forward * 3000);
			/*Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			aimPos.z = 0;
    [SerializeField]
	private GameObject FireBall;
    
	void Update () {

        
        if (Input.GetKeyDown(KeyCode.E)){	
			var projectile = Instantiate(FireBall, SpawnPos.transform.position, Quaternion.identity);


            SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform);
            Destroy(projectile.gameObject, 5f);	
		}
		
	}
}
