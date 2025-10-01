using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    protected override bool ShouldStartSpawning() => false;

    public void SpawnBombAtPosition(Vector3 position)
    {
        Bomb bomb = ObjectPool.GetObject();
        SubscribeToObjectEvents(bomb);
        bomb.transform.position = position;
        bomb.ActivateBomb(position);
    }

    protected override void SubscribeToObjectEvents(Bomb bomb)
    {
        bomb.BombExploded += OnHandleBombExploded;
    }

    protected override void UnsubscribeFromObjectEvents(Bomb bomb)
    {
        bomb.BombExploded -= OnHandleBombExploded;
    }

    protected override void ResetObject(Bomb bomb)
    {
    }

    protected override void HandleObjectExpired(Bomb bomb)
    {
        ObjectPool.ReturnObject(bomb);
    }

    private void OnHandleBombExploded(Bomb bomb)
    {
        UnsubscribeFromObjectEvents(bomb);
        HandleObjectExpired(bomb);
    }
}