using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [HideInInspector] public MeshRenderer meshRenderer;
    private MaterialPropertyBlock matBlock;
    private Camera mainCamera;

    void Start(){
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        mainCamera = Camera.main;
    }

    void FixedUpdate() {
        AlignCamera();
    }

    public void UpdateFill(float _fill){
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", _fill);
        meshRenderer.SetPropertyBlock(matBlock);
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
