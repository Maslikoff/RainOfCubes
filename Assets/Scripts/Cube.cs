using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> OnCubeTouchedPlatform;
    public event Action<Cube> OnCubeExpired;

    [SerializeField] private Color _initialColor = Color.blue;
    [SerializeField] private Color _touchedColor = Color.red;
    [SerializeField] private Vector2 _rangeLife = new Vector2(2, 5);

    private Renderer _renderer;
    private bool _hasTouchedPlatform = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        ResetCube();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform || collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _hasTouchedPlatform = true;
        _renderer.material.color = _touchedColor;

        OnCubeTouchedPlatform?.Invoke(this);

        StartCoroutine(CountdownToReturn());
    }

    public void ResetCube()
    {
        _renderer.material.color = _initialColor;
        _hasTouchedPlatform = false;
    }

    private IEnumerator CountdownToReturn()
    {
        float lifetime = UnityEngine.Random.Range(_rangeLife.x, _rangeLife.y);

        yield return new WaitForSeconds(lifetime);

        OnCubeExpired?.Invoke(this);
    }
}