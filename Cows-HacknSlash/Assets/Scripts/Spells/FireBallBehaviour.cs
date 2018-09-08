using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour {
    public float Speed = 2f;

    private Rigidbody _rigidbody;
	
	void Start() {
		_rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	void FixedUpdate() {
        //_rigidbody.AddForce(transform.forward * (Speed + 2));
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _rigidbody.AddForce(ray.direction * Speed, ForceMode.Impulse);
	}
}
