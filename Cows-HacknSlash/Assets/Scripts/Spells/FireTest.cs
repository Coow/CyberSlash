using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour {

    [SerializeField]
    [Range (1, 45)]
    private float FiringAngle = 45f;

    [SerializeField]
    private float Gravity = 9.8f;

    [SerializeField]
    private float WaitTime = 0f;

    private Vector3 _target;
	
    public void Shoot(Transform spawnPoint, bool useMouseInput) {
         StartCoroutine(FireMovement(spawnPoint, useMouseInput));
    }

    private IEnumerator FireMovement(Transform spawnPoint, bool useMouseInput) {
        yield return new WaitForSeconds(WaitTime);
        spawnPoint.position = transform.position;

        if (useMouseInput)
        {
            // Getting the mouse input position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Using a simple plane cause our camera is on top.
            Plane plane = new Plane(Vector3.up, Vector3.zero); 
            float dist;
            if (plane.Raycast(ray, out dist))
            {
                _target = ray.GetPoint(dist);
            }
        }

        // Calculate distance to target.
        float target_Distance = Vector3.Distance(spawnPoint.position, _target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float velocity = target_Distance / (Mathf.Sin(2 * FiringAngle * Mathf.Deg2Rad) / Gravity);

        // Extract the X  Y componenent of the velocity.
        float x = Mathf.Sqrt(velocity) * Mathf.Cos(FiringAngle * Mathf.Deg2Rad);
        float y = Mathf.Sqrt(velocity) * Mathf.Sin(FiringAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDutation = target_Distance / 2;

        // Rotate object to face the target.
        spawnPoint.rotation = Quaternion.LookRotation(_target - spawnPoint.position);

        float elapsed_time = 0;

        while (elapsed_time < flightDutation) {
            if (spawnPoint != null) {
                spawnPoint.Translate(0f, (y - (Gravity * elapsed_time)) * Time.deltaTime, x * Time.deltaTime);
            }
            elapsed_time += Time.deltaTime;
            yield return null;
        }

    }

    public void FollowTarget(Vector3 target)
    {
        this._target = target;
    }

}
