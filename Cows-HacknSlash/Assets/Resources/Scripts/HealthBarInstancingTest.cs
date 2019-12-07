using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock matBlock;
    [Range(0f,1f)]
    public float fill = 1;

    Camera mainCamera;

    void Start(){
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        mainCamera = Camera.main;
    }

    void Update(){
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", fill);
        meshRenderer.SetPropertyBlock(matBlock);

        AlignCamera();
    }

    private void AlignCamera() {
        if (mainCamera != null) {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

}
