using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private BombPool _bombPool;
    [SerializeField] private CubeStatisticsDisplay _cubeStatistics;
    [SerializeField] private BombStatisticsDisplay _bombStatistics;

    private void Start()
    {
        _cubeStatistics.Initialize(_cubePool);
        _bombStatistics.Initialize(_bombPool);
    }
}