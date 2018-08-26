using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorKiller : MonoBehaviour {

	public float LifeTime = 0.1f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, LifeTime);
	}

}
