using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	[SerializeField]
	private GameObject spawnPos;

	[Header("FireBall")]
	public GameObject fireball;
	public float fireBallSpeed;

	void Update () {

		if(Input.GetKeyDown(KeyCode.E)){	
			Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			aimPos.z = 0;

			var projectile = Instantiate(fireball,spawnPos.transform.position,Quaternion.identity);
			projectile.transform.LookAt(aimPos);
			Debug.Log(aimPos);

			projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * (fireBallSpeed * Time.deltaTime));		
		}
	}
}
