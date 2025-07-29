using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _spawnInterval = .5f;
    [SerializeField] private float _spawnHeight = 15f;
    [SerializeField] private float _spawnAreaSize = 10f;

    private WaitForSeconds _spawnWait;
    private bool _isRaining = true;

    private void Start()
    {
        _spawnWait = new WaitForSeconds(_spawnInterval);

        StartCoroutine(SpawnCubesRoutine());
    }

    private IEnumerator SpawnCubesRoutine()
    {
        while (_isRaining)
        {
            SpawnCube();

            yield return _spawnWait;
        }
    }

    private void SpawnCube()
    {
        Cube cube = _cubePool.GetCube();
        cube.CubeExpired += OnHandleCubeExpired;

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

    private void OnHandleCubeExpired(Cube cube)
    {
        cube.CubeExpired -= OnHandleCubeExpired;
        _cubePool.ReturnCube(cube);
    }
}