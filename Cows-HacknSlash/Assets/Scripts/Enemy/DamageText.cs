using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{   
    public float damage;
    public float lifeTime = 1f;
    public Color color;
    public bool bold;
    private Camera mainCamera;
    private TextMeshPro tmp_text;


    void Start(){
        mainCamera = Camera.main;
        tmp_text = GetComponent<TextMeshPro>();
        tmp_text.text = damage.ToString();

        tmp_text.color = color;

        if(bold){
            tmp_text.fontStyle = FontStyles.Bold;
        } else {
            tmp_text.fontStyle = FontStyles.Normal;
        }

        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate() {
        AlignCamera();
        
        gameObject.transform.localScale  -= new Vector3(0.1F, .1f, .1f) * (lifeTime * 3) * Time.deltaTime;

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
