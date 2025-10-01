using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    protected override void SubscribeToObjectEvents(Cube cube)
    {
        cube.CubeExpired += OnHandleCubeExpired;
        cube.TestingAndBuildingBomb += _bombSpawner.SpawnBombAtPosition;
    }

    protected override void UnsubscribeFromObjectEvents(Cube cube)
    {
        cube.CubeExpired -= OnHandleCubeExpired;
        cube.TestingAndBuildingBomb -= _bombSpawner.SpawnBombAtPosition;
    }

    protected override void ResetObject(Cube cube)
    {
        cube.ResetCube();
    }

    protected override void HandleObjectExpired(Cube cube)
    {
        ObjectPool.ReturnObject(cube);
    }

    private void OnHandleCubeExpired(Cube cube)
    {
        UnsubscribeFromObjectEvents(cube);
        HandleObjectExpired(cube);
    }
}
