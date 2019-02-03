using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {
	public ParticleSystem particleSystem;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerStay(Collider other){
		if(other.gameObject.transform != gameObject.transform.parent) {
			particleSystem.gameObject.SetActive(true);
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.transform != gameObject.transform.parent) {
			particleSystem.gameObject.SetActive(true);
		}
	}
}
