using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpawnController {

    [Header("Rat Spawn")]
    // Amount of rats to be spawned.
    [SerializeField] private float RatAmount = 5;           
    [SerializeField] private GameObject RatPrefab;

    // Spawn Rats at specified points.
    public void SpawnRat(List<Transform> spawnPoints)
    {
        for (int i = 0; i < RatAmount; i++)
        {
            var randomPoint = Random.Range(0, spawnPoints.Count);
            Instantiate(RatPrefab, spawnPoints[randomPoint].position, Quaternion.identity);
        }
    }
}
