using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {
	Rigidbody _rigidbody;

	public float speed;
	void Start() {
		_rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	void FixedUpdate() {
		
	}
}
