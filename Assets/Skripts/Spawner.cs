using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = .5f;
    [SerializeField] private float _spawnHeight = 15f;
    [SerializeField] private float _spawnAreaSize = 10f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnCube();
            _timer = 0f;
        }
    }

    private void SpawnCube()
    {
        Vector3 spawnPosition = new Vector3(
               Random.Range(-_spawnAreaSize, _spawnAreaSize),
            _spawnHeight,
            Random.Range(-_spawnAreaSize, _spawnAreaSize)
        );

        GameObject cube = CubePool.Instance.GetCube();
        cube.transform.position = spawnPosition;
        cube.GetComponent<CubeBehavior>().ResetCube();
    }
}
