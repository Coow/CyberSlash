using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour {

    [Header("Navmesh Surfaces")]
    [Tooltip("Drag all the surfaces you want to bake at runtime here")]
    public NavMeshSurface[] surfaces;
    
    void Start () 
    {
        for (int i = 0; i < surfaces.Length; i++) 
        {
            surfaces [i].BuildNavMesh();   
        }   
    }

}
