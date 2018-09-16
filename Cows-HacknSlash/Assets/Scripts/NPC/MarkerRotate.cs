using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MarkerRotate : MonoBehaviour {

	public float TurnSpeed = 120f;

	void Start()
	{
		// For spawning on the server. Not in use now. 
		// NetworkServer.Spawn(gameObject);
	}
	void Update () {
		transform.Rotate(0, (Time.deltaTime * TurnSpeed),0);
	}
}
