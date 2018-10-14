using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeHitObject : MonoBehaviour {

	private Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}
	// Yes.. We need to use Awake here
	void Awake () {
		animator.SetTrigger("SHAKE");
	}
}
