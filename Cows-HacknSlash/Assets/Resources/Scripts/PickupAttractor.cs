using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAttractor : MonoBehaviour {

    public GameObject attractorParent;
    public float startMoveSpeed;
    public float accelRate = 1f;
    public float maxMoveSpeed = 50f;

    private bool moveToPlayer;
    private float moveTimer;
    private Rigidbody attractorPRB;
    private GameObject movingTarget;

    private void Awake()
    {
        attractorPRB = attractorParent.GetComponent<Rigidbody>();
    }

    void Start ()
    {		
	}

	void Update ()
    {
		if (moveToPlayer && movingTarget != null)
        {
            moveTimer += Time.deltaTime;

            float step = (accelRate * moveTimer) + startMoveSpeed;
            step = Mathf.Clamp(step, 0, maxMoveSpeed);
            attractorParent.transform.position = Vector3.MoveTowards(transform.position, movingTarget.transform.position, (step * Time.deltaTime));
        }
	}

    private void OnTriggerEnter(Collider enteredCol)
    {
        if (enteredCol.gameObject.CompareTag("Player"))
        {
            attractorPRB.useGravity = false;
            movingTarget = enteredCol.gameObject;
            moveToPlayer = true;
            moveTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider exitedCol)
    {
        if (exitedCol.gameObject.CompareTag("Player"))
        {
            attractorPRB.useGravity = true;
            movingTarget = null;
            moveToPlayer = false;
        }
    }
}
