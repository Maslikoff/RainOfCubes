using UnityEngine;
using TMPro;

public abstract class StatisticsDisplay<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI TotalSpawnedText;
    [SerializeField] protected TextMeshProUGUI TotalCreatedText;
    [SerializeField] protected TextMeshProUGUI ActiveCountText;

    [SerializeField] protected string ObjectName = "Object";

    protected ObjectPool<T> _targetPool;

    public virtual void Initialize(ObjectPool<T> pool)
    {
        _targetPool = pool;
        _targetPool.StatisticsChanged += OnStatisticsChanged;

        UpdateDisplay(_targetPool.TotalSpawned, _targetPool.TotalCreated, _targetPool.ActiveCount);
    }

    protected virtual void OnStatisticsChanged(int totalSpawned, int totalCreated, int activeCount)
    {
        UpdateDisplay(totalSpawned, totalCreated, activeCount);
    }

    protected virtual void UpdateDisplay(int totalSpawned, int totalCreated, int activeCount)
    {
        TotalSpawnedText.text = $"{ObjectName}s Spawned: {totalSpawned}";
        TotalCreatedText.text = $"{ObjectName}s Created: {totalCreated}";
        ActiveCountText.text = $"Active {ObjectName}s: {activeCount}";
    }

    protected virtual void OnDestroy()
    {
        if (_targetPool != null)
            _targetPool.StatisticsChanged -= OnStatisticsChanged;
    }
}