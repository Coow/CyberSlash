using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	[SerializeField]
	private GameObject SpawnPos;

	[Header("Spells")]
    [SerializeField]
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
		}
	}
}