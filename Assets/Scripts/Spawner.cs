using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _spawnInterval = .5f;
    [SerializeField] private float _spawnHeight = 15f;
    [SerializeField] private float _spawnAreaSize = 10f;

    private bool _isRaining = true;

    private void Start()
    {
        StartCoroutine(SpawnCubesRoutine());
    }

    private IEnumerator SpawnCubesRoutine()
    {
        yield return new WaitForSeconds(_spawnInterval);

        while (_isRaining)
        {
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        Cube cube = _cubePool.GetCube();
        cube.OnCubeExpired += HandleCubeExpired;

        cube.transform.position = GetRandomSpawnPosition();
        cube.ResetCube();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-_spawnAreaSize, _spawnAreaSize),
            _spawnHeight,
            Random.Range(-_spawnAreaSize, _spawnAreaSize)
        );
    }

    private void HandleCubeExpired(Cube cube)
    {
        cube.OnCubeExpired -= HandleCubeExpired;
        _cubePool.ReturnCube(cube);
    }
}