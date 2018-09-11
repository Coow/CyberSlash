using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Dialogue[] Dialogues;
	public int DialogueSelected;
	public float DialogueCharacterTimer;
	public float MaxDialogueCharacterTimer;
	public int CurrentCharacter;
	private Text dialogueText;

	// Use this for initialization
	void Start () {
		
		DialogueCharacterTimer = MaxDialogueCharacterTimer;
		dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (DialogueCharacterTimer > 0f){

			DialogueCharacterTimer -= Time.deltaTime;
		}else{

			if (DialogueSelected > -1 && Dialogues[DialogueSelected].dialogue.Length > CurrentCharacter){

				dialogueText.text += Dialogues[DialogueSelected].dialogue[CurrentCharacter];
				CurrentCharacter++;
				DialogueCharacterTimer = MaxDialogueCharacterTimer;
			}
		}
	}
}
