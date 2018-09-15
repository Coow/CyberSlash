using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	[SerializeField]
	private GameObject SpawnPos;

	[Header("FireBall")]
    [SerializeField]
	private GameObject FireBall;
	[Tooltip("0 == null; 1 == FireStaff")]
	[SerializeField]
	private int StaffSelected;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (StaffSelected == 1)
            {
                var projectile = Instantiate(FireBall, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
            }
        }
    }
}