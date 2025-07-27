using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private CubeBehavior _cubePrefab;
    [SerializeField] private int _initialPoolSixe = 20;

    private Queue<CubeBehavior> _pooledCubes = new Queue<CubeBehavior>();

    private void Awake()
    {
        InitializePool();
    }

    public CubeBehavior GetCube()
    {
        if (_pooledCubes.Count == 0)
            return InitCube();

        CubeBehavior newCube = _pooledCubes.Dequeue();
        newCube.gameObject.SetActive(true);

        return newCube;
    }

    public void ReturnCube(CubeBehavior cube)
    {
        cube.gameObject.SetActive(false);
        _pooledCubes.Enqueue(cube);
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialPoolSixe; i++)
            InitCube();
    }

    private CubeBehavior InitCube()
    {
        CubeBehavior cube = Instantiate(_cubePrefab);
        cube.gameObject.SetActive(false);
        cube.transform.SetParent(transform);
        cube.SetPool(this);
        _pooledCubes.Enqueue(cube);

        return cube;
    }
}
