using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{

    [SerializeField]
    private GameObject SpawnPos;

    [Header("FireBall")]
    [SerializeField]
    private GameObject FireBall;

    [SerializeField]
    private GameObject ice;
    [SerializeField]
    private GameObject poison;
    [Tooltip("0 == null; 1 == FireStaff")]
    [SerializeField]
    private int StaffSelected;

    void Update()
    {
        if (Inventory.itemselected == "Wand")
        {
            StaffSelected = 1;
        }
        else if (Inventory.itemselected != "Wand")
        {
            StaffSelected = 0;
        }
        if (StaffSelected == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var projectile = Instantiate(FireBall, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                var projectile = Instantiate(ice, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {  
                var projectile = Instantiate(poison, SpawnPos.transform.position, Quaternion.identity);
                SpawnPos.GetComponent<FireTest>().Shoot(projectile.transform, true);
                Destroy(projectile.gameObject, 5f);
            }
        }


    }
}