using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	[SerializeField]
	private GameObject SpawnPos;

	[Header("Spells")]
    [SerializeField]
<<<<<<< HEAD
	private GameObject fireball;
	[SerializeField]
	private GameObject ice;
    
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.E)){	
			var projectile = Instantiate(fireball, SpawnPos.transform.position, Quaternion.identity);

            SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform);
            Destroy(projectile.gameObject, 5f);	
		}
		else if (Input.GetKeyDown(KeyCode.R)){	
			var projectile = Instantiate(ice, SpawnPos.transform.position, Quaternion.identity);

            SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform);
            Destroy(projectile.gameObject, 5f);	
=======
	private GameObject FireBall;
	[Tooltip("0 == null; 1 == FireStaff")]
	[SerializeField]
	private int StaffSelected;
    
	void Update () {
      if (Input.GetKeyDown(KeyCode.E)) {	
			
			if (StaffSelected == 1) {
			  var projectile = Instantiate(FireBall, SpawnPos.transform.position, Quaternion.identity);
        SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform);
        Destroy(projectile.gameObject, 5f);
			}
>>>>>>> upstream/master
		}
	}
}