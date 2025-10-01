using System.Collections;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected ObjectPool<T> ObjectPool;
    [SerializeField] private float SpawnInterval = .5f;
    [SerializeField] private float SpawnHeight = 15f;
    [SerializeField] private float SpawnAreaSize = 10f;

    protected WaitForSeconds SpawnWait;
    protected bool IsSpawning = true;

    protected virtual void Start()
    {
        SpawnWait = new WaitForSeconds(SpawnInterval);

        if (ShouldStartSpawning())
            StartCoroutine(SpawnCubesRoutine());
    }

    protected virtual bool ShouldStartSpawning() => true;

    protected virtual IEnumerator SpawnCubesRoutine()
    {
        while (IsSpawning)
        {
            SpawnObject();

            yield return SpawnWait;
        }
    }

    protected virtual void SpawnObject()
    {
        T obj = ObjectPool.GetObject();
        SubscribeToObjectEvents(obj);

        obj.transform.position = GetRandomSpawnPosition();
        ResetObject(obj);
    }

    protected virtual Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-SpawnAreaSize, SpawnAreaSize),
            SpawnHeight,
            Random.Range(-SpawnAreaSize, SpawnAreaSize)
        );
    }

    protected abstract void SubscribeToObjectEvents(T obj);
    protected abstract void UnsubscribeFromObjectEvents(T obj);
    protected abstract void ResetObject(T obj);
    protected abstract void HandleObjectExpired(T obj);
}