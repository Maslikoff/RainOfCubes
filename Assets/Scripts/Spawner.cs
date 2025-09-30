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

    [Header("UI - Cubes")]
    [SerializeField] private TextMeshProUGUI _cubesTotalSpawnedText;
    [SerializeField] private TextMeshProUGUI _cubesTotalCreatedText;
    [SerializeField] private TextMeshProUGUI _cubesActiveCountText;

    [Header("UI - Bombs")]
    [SerializeField] private TextMeshProUGUI _bombsTotalSpawnedText;
    [SerializeField] private TextMeshProUGUI _bombsTotalCreatedText;
    [SerializeField] private TextMeshProUGUI _bombsActiveCountText;

    private WaitForSeconds _spawnWait;
    private bool _isRaining = true;

    private void Start()
    {
        _spawnWait = new WaitForSeconds(_spawnInterval);

        StartCoroutine(SpawnCubesRoutine());
    }

    private void Update()
    {
        UpdateStatistics();
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

    private void UpdateStatistics()
    {
        _cubesTotalSpawnedText.text = $"Total Spawned: {_cubePool.TotalSpawned}";
        _cubesTotalCreatedText.text = $"Total Created: {_cubePool.TotalCreated}";
        _cubesActiveCountText.text = $"Active: {-_cubePool.ActiveCount}";

        _bombsTotalSpawnedText.text = $"Total Spawned: {_bombPool.TotalSpawned}";
        _bombsTotalCreatedText.text = $"Total Created: {_bombPool.TotalCreated}";
        _bombsActiveCountText.text = $"Active: {-_bombPool.ActiveCount}";
    }
}