using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    public DialogueManager mg;

    void Update(){
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            TriggerDialogue();
        }
    }

	public void TriggerDialogue ()
	{
        Debug.Log("Button clickd!");
		mg.GetComponent<DialogueManager>().StartDialogue(dialogue);
	}

}