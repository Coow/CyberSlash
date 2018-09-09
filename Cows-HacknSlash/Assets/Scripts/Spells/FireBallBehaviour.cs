using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour {
    public float speed = 2f;

    private Rigidbody _rigidbody;
	
	void Start() {
		_rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.AddForce(this.transform.forward * speed);
	}
	void FixedUpdate() {
        //_rigidbody.AddForce(transform.forward * (Speed + 2));
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _rigidbody.MovePosition(new Vector3(this.transform.position.x, 2f, this.transform.position.z));
	}
}
