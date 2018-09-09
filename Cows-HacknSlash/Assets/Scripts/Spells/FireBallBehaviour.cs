using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour {
    public float Speed;

    private Rigidbody _rigidbody;
    private Vector3 direction;

    void Start() {
		_rigidbody = gameObject.GetComponent<Rigidbody>();
        Fire();
        Destroy(this.gameObject, 5f);
	}

    private void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float dist;
        if (plane.Raycast(ray, out dist))
        {
            var target = ray.GetPoint(dist);
            direction = transform.position - target;
            _rigidbody.AddForce(-direction * Speed);
        }
    }
}
