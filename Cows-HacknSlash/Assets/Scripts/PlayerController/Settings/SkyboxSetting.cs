using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Skybox))]
public class SkyboxSetting : MonoBehaviour {

	public bool skyboxEnabled = true;
	private Camera camera;
	private Skybox skybox;

	void Start() {
		camera = GetComponent<Camera>();
		skybox = GetComponent<Skybox>();
	}

	void FixedUpdate() {
		if(skyboxEnabled) {
			skybox.enabled = true;
			camera.clearFlags = CameraClearFlags.Skybox;
			Debug.Log("Skybox is enabled");
		}

		if(!skyboxEnabled) {
			camera.clearFlags = CameraClearFlags.SolidColor;
			skybox.enabled = false;
			Debug.Log("Skybox disabled");
		}
	}
}
