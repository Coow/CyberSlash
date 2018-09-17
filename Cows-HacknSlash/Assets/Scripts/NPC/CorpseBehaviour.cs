using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBehaviour : MonoBehaviour {

    [SerializeField]
    private List<Transform> SpawnPoints;

    private Renderer _renderer;
    private ISpawnController _spawnController;

    private bool _hasSpawn = false;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _spawnController = GameObject.Find("Spawn_Manager").GetComponent<ISpawnController>();
    }

    // Update is called once per frame.
    void Update () {

        // Check if this object (corpse) is being renderer by Camera.
        if (RendererExtensions.IsVisibleFrom(_renderer, Camera.main))
        {
            if (!_hasSpawn)
            {
                _spawnController.SpawnRat(SpawnPoints);
                _hasSpawn = true;
            }
        }
	}
}
