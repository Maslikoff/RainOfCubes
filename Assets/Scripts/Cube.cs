using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.blue;
    [SerializeField] private Color _touchedColor = Color.red;
    [SerializeField] private Vector2 _rangeLife = new Vector2(2, 5);

    private Renderer _renderer;
    private Quaternion _initialRotation;
    private bool _hasTouchedPlatform = false;

    public event Action<Cube> CubeTouchedPlatform;
    public event Action<Cube> CubeExpired;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _initialRotation = transform.rotation;

        ResetCube();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform || collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;


        _hasTouchedPlatform = true;
        _renderer.material.color = _touchedColor;

        CubeTouchedPlatform?.Invoke(this);

        StartCoroutine(CountdownToReturn());
    }

    public void ResetCube()
    {
        _renderer.material.color = _initialColor;
        transform.rotation = _initialRotation;
        _hasTouchedPlatform = false;
    }

    private IEnumerator CountdownToReturn()
    {
        float lifetime = UnityEngine.Random.Range(_rangeLife.x, _rangeLife.y);

        yield return new WaitForSeconds(lifetime);

        CubeExpired?.Invoke(this);
    }
}