using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTrigger : MonoBehaviour {

    public List<GameObject> objectsInZone;

    public float experienceValue;
    public float goldValue;

    private float experienceToGive;

    void Start ()
    {
	}

	void Update ()
    {
        // HACK Debug.
        if (Input.GetKeyDown(KeyCode.X))
        {
            GiveDrops();
        }
		
	}

    private void OnTriggerEnter(Collider enteredCol)
    {
        if (enteredCol.gameObject.CompareTag("Player"))
        {
            objectsInZone.Add(enteredCol.gameObject);
        }
    }

    private void OnTriggerExit(Collider exitedCol)
    {
        if (exitedCol.gameObject.CompareTag("Player"))
        {
            objectsInZone.Remove(exitedCol.gameObject);
        }
    }

    public void GiveDrops()
    {
        // Removes objects from the list that were destroyed while in the trigger zone. 
        for (int i = objectsInZone.Count - 1; i > -1; i--)
        {
            if (objectsInZone[i] == null)
            {
                objectsInZone.RemoveAt(i);
            }
        }

        if (objectsInZone.Count > 0)
        {
            // We probably want to provide some bonus to larger parties when it comes to experience earnings. Otherwise there won't be much of a benefit to larger parties.
            experienceToGive = (experienceValue / objectsInZone.Count);

            foreach (GameObject currentObj in objectsInZone)
            {
                PlayerManager currentScript = currentObj.GetComponent<PlayerManager>();
                currentScript.Experience += experienceToGive;
            }       
        }
    }


}
