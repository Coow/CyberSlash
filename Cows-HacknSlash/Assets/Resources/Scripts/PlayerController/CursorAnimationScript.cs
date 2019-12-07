using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimationScript : MonoBehaviour {

	public float lifeTime;
	// Use this for initialization.
	void Start () {
		Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame.
	void Update () {
	}
}
