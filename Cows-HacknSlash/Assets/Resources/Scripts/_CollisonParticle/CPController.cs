using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPController : MonoBehaviour {

	public GameObject blade;
	
	public float rotation;
	
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rotation = Input.GetAxis("Horizontal") * 10;

		gameObject.transform.Rotate(0, rotation, 0);
	}
}
