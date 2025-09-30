using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int InitialPoolSize = 20;

    protected Queue<T> PooledObjects = new Queue<T>();

    public int TotalSpawned { get; protected set; } = 0;
    public int TotalCreated { get; protected set; } = 0;
    public int ActiveCount => TotalSpawned - PooledObjects.Count;

    public event Action<int, int, int> StatisticsChanged;

    private void Awake()
    {
        InitializePool();
    }

    public virtual T GetObject()
    {
        if (PooledObjects.Count > 0)
            CreateNewObject();

        T obj = PooledObjects.Dequeue();
        obj.gameObject.SetActive(true);
        TotalSpawned++;

        StatisticsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);

        return obj;
    }

    public virtual void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        ResetObject(obj);
        PooledObjects.Enqueue(obj);

        StatisticsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);
    }

    protected virtual void InitializePool()
    {
        for (int i = 0; i < InitialPoolSize; i++)
            CreateNewObject();
    }

    protected virtual void CreateNewObject()
    {
        T obj = Instantiate(Prefab);
        obj.gameObject.SetActive(false);
        PooledObjects.Enqueue(obj);
        TotalCreated++;

        StatisticsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);
    }

    protected abstract void ResetObject(T obj);
}
