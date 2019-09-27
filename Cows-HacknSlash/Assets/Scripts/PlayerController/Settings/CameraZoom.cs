using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class CameraZoom : MonoBehaviour
{   
    [SerializeField]
    private float sensitivity ;
    [SerializeField]
    private CinemachineFreeLook cinemachineFreeLook;
    private float cameraHeight;
    private float f;
    
    void Start()
    {
        cameraHeight = cinemachineFreeLook.m_YAxis.m_InputAxisValue;
    }

      //Update is called once per frame
    void Update()
    {
        cameraHeight = Input.GetAxis("Mouse ScrollWheel");    
    }
}
