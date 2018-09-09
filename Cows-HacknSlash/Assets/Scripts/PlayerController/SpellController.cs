using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	[SerializeField]
	private GameObject SpawnPos;

	[Header("FireBall")]
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
