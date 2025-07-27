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
        while (_isRaining)
        {
            SpawnCube();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnCube()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-_spawnAreaSize, _spawnAreaSize),
            _spawnHeight,
            Random.Range(-_spawnAreaSize, _spawnAreaSize)
        );

        CubeBehavior cube = _cubePool.GetCube();
        cube.transform.position = spawnPosition;
        cube.ResetCube();
    }
}
