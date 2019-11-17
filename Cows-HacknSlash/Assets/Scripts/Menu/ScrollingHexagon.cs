using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingHexagon : MonoBehaviour
{   
    [SerializeField]
    private float target,distance;
    private Vector3 startPos;
    void Start(){
        startPos = transform.position;
    }
    void Update()
    {  
        transform.position = transform.position + new Vector3(distance / 10, 0 ,0);
        if(transform.position.x >= target){
            transform.position = startPos;
        }
    }
}
