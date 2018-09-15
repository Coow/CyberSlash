using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpawnController {

    [Header("Rat Spawn")]
    [SerializeField] private float RatAmount = 5;           // Amount of rate to be spawn.
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
