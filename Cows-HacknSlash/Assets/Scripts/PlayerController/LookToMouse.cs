using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToMouse : MonoBehaviour
{
    void Update()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out _hit)) {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }
}
