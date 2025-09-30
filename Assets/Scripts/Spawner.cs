using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private BombPool _bombPool;
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

    public void SpawnCube()
    {
        Cube cube = _cubePool.GetObject();
        cube.CubeExpired += OnHandleCubeExpired;
        cube.CheckAndCreateBomb += OnCreateBombAtPosition;

        cube.transform.position = GetRandomSpawnPosition();
        cube.ResetCube();
    }

    public void SpawnBomb(Vector3 position)
    {
        Bomb bomb = _bombPool.GetObject();
        bomb.BombExploded += OnHandleBombExploded;

        bomb.transform.position = position;
        bomb.ActivateBomb(position);
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
        cube.CheckAndCreateBomb -= OnCreateBombAtPosition;

        _cubePool.ReturnObject(cube);
    }

    private void OnHandleBombExploded(Bomb bomb)
    {
        bomb.BombExploded -= OnHandleBombExploded;
        _bombPool.ReturnObject(bomb);
    }

    private void OnCreateBombAtPosition(Vector3 position)
    {
        SpawnBomb(position);
    }
}