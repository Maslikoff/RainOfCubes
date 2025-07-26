using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _initialPoolSixe = 20;

    private Queue<GameObject> _pooledCubes = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    public GameObject GetCube()
    {
        if (_pooledCubes.Count == 0)
            InitCube();

        GameObject newCube = _pooledCubes.Dequeue();
        newCube.SetActive(true);

        return newCube;
    }

    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        _pooledCubes.Enqueue(cube);
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialPoolSixe; i++)
            InitCube();
    }

    private GameObject InitCube()
    {
        GameObject cube = Instantiate(_cubePrefab);
        cube.SetActive(false);
        cube.GetComponent<CubeBehavior>().OnReturnToPool += () => ReturnCube(cube);
        _pooledCubes.Enqueue(cube);

        return cube;
    }
}
