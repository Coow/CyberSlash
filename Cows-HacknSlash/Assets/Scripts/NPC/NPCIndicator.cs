using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCStates {Quest = 0, Note = 1, Passive = 2}

public class NPCIndicator : MonoBehaviour {

    public GameObject QuestionMark;
	
    public GameObject ExlamationMark; 

	public NPCStates CurState = NPCStates.Passive;

	[SerializeField]
	private Vector3 spawnPosition;
	// Public due to it might getting called from different sources.
	// QuestManager for example.
	[HideInInspector]
	public bool spawned;
	
	void FixedUpdate () {
		// Switch states are basiclly If statements, just.. easier.
		// Might be a problem down the line with SQL or another database structure however. 
		switch (CurState)
        {
            case NPCStates.Quest:
			{	
				if (!spawned)
				{
					GameObject marker = Instantiate(QuestionMark,new Vector3(), Quaternion.identity);
					marker.transform.parent = gameObject.transform;
					// Set the Prefab above the NPC.
					marker.transform.position = (gameObject.transform.position + spawnPosition);
					// If the prefab has a different scale, this can be done here.
					// marker.transform.localScale = new Vector3(0.001f,0.001f,0.001f);
					// Set the tag of the GameObject, for later use.
					marker.tag = "NPCMarker";
					spawned = true;
				}
			}
			break;
			case NPCStates.Note:
			{	
				if (!spawned)
				{	
					GameObject marker = Instantiate(ExlamationMark,new Vector3(), Quaternion.identity);
					marker.transform.parent = gameObject.transform;
					// Set the Prefab above the NPC.
					marker.transform.position = (gameObject.transform.position + spawnPosition);
					// If the prefab has a different scale, this can be done here.
					// marker.transform.localScale = new Vector3(0.001f,0.001f,0.001f);
					// Set the tag of the GameObject, for later use.
					marker.tag = "NPCMarker";
					spawned = true;
				}
			}
			break;
			case NPCStates.Passive:
			{
				DestroyMarker();
			}
			break;
        }
	}
	public void DestroyMarker (){
		// Get the hierachy of this Game Object.
		Transform parentTransform = this.gameObject.transform;
		spawned = false;

		// Runs through every child of the NPC/Game object, and checks for NPCMarker tag, and deletes that one.
		foreach (Transform child in parentTransform)
			if(child.CompareTag ("NPCMarker")){
				Destroy(child.gameObject);
			}
	}
}
