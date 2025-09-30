using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private Vector2 _fadeTimeRange = new Vector2(2, 5);

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Material _material;

    private bool _isExploded = false;

    public event Action<Bomb> BombExploded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        CreatingMaterial();
    }

    public void ActivateBomb(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _isExploded = false;

        _rigidbody.velocity = Vector3.one;
        _rigidbody.angularVelocity = Vector3.zero;

        StartCoroutine(FadeAndExplode());
    }

    public void ResetBomb()
    {
        StopAllCoroutines();
        _material.color = Color.black;
        _isExploded = false;
    }

    private IEnumerator FadeAndExplode()
    {
        float fadeTime = UnityEngine.Random.Range(_fadeTimeRange.x, _fadeTimeRange.y);
        float elapsedTime = 0f;

        Color startColor = Color.black;
        Color endColor = new Color(0, 0, 0, 0);

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeTime;
            _material.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        if (_isExploded)
            return;

        _isExploded = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb != _rigidbody)
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 1f, ForceMode.Impulse);
        }

        BombExploded?.Invoke(this);
    }

    private void CreatingMaterial()
    {
        _material = new Material(_renderer.material);
        _material.color = Color.black;
        _material.SetFloat("_Mode", 2);
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = 3000;

        _renderer.material = _material;
    }
}
