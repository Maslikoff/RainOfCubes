using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.blue;
    [SerializeField] private Color _touchedColor = Color.red;
    [SerializeField] private Vector2 _rangeLife = new Vector2(2, 5);

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Quaternion _initialRotation;

    private bool _hasTouchedPlatform = false;
    [SerializeField] private bool _shouldSpawnBomb = false;

    public event Action<Cube> CubeTouchedPlatform;
    public event Action<Cube> CubeExpired;
    public event Action<Vector3> CheckAndCreateBomb;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        _initialRotation = transform.rotation;

        ResetCube();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform)
            return;

        bool hasPlatform = collision.gameObject.TryGetComponent<Platform>(out _);
        bool hasIrritantBomb = collision.gameObject.TryGetComponent<IrritantBomb>(out _);

        _hasTouchedPlatform = true;
        _renderer.material.color = _touchedColor;
        _shouldSpawnBomb = hasIrritantBomb;

        CubeTouchedPlatform?.Invoke(this);

        StartCoroutine(CountdownToReturn());
    }

    public void ResetCube()
    {
        _renderer.material.color = _initialColor;
        _hasTouchedPlatform = false;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.Sleep();

        transform.rotation = _initialRotation;
    }

    private IEnumerator CountdownToReturn()
    {
        float lifetime = UnityEngine.Random.Range(_rangeLife.x, _rangeLife.y);

        yield return new WaitForSeconds(lifetime);

        if (_shouldSpawnBomb)
            CheckAndCreateBomb?.Invoke(transform.position);

        CubeExpired?.Invoke(this);
    }
}