using System.Collections.Generic;
using UnityEngine;

public class CubePool : ObjectPool<Cube>
{
    protected override void ResetObject(Cube cube)
    {
        cube.ResetCube();
    }

    /*[SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initialPoolSixe = 20;

    private Queue<Cube> _pooledCubes = new Queue<Cube>();

    private void Awake()
    {
        InitializePool();
    }

    public Cube GetCube()
    {
        if (_pooledCubes.Count == 0)
            CreateNewCube();

        Cube cube = _pooledCubes.Dequeue();
        cube.gameObject.SetActive(true);

        return cube;
    }

    public void ReturnCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.ResetCube();
        _pooledCubes.Enqueue(cube);
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialPoolSixe; i++)
            CreateNewCube();
    }

    private void CreateNewCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.gameObject.SetActive(false);
        _pooledCubes.Enqueue(cube);
    }*/
}