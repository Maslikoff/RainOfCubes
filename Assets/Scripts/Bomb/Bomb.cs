using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(VisualEffect))]
[RequireComponent(typeof(ExplosionEffect))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private Vector2 _fadeTimeRange = new Vector2(2, 5);

    private Rigidbody _rigidbody;
    private VisualEffect _visualEffect;
    private ExplosionEffect _explosionEffect;

    private bool _isExploded = false;

    public event Action<Bomb> BombExploded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _visualEffect = GetComponent<VisualEffect>();
        _explosionEffect = GetComponent<ExplosionEffect>();
    }

    public void ActivateBomb(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _isExploded = false;

        _rigidbody.velocity = Vector3.one;
        _rigidbody.angularVelocity = Vector3.zero;

        _visualEffect.StartFadeEffect(_fadeTimeRange);

        StartCoroutine(FadeAndExplode());
    }

    public void ResetBomb()
    {
        StopAllCoroutines();

        _visualEffect.ResetEffect();
        _isExploded = false;
    }

    private IEnumerator FadeAndExplode()
    {
        float fadeTime = UnityEngine.Random.Range(_fadeTimeRange.x, _fadeTimeRange.y);
        yield return new WaitForSeconds(fadeTime);

        Explode();
    }

    private void Explode()
    {
        if (_isExploded)
            return;

        _isExploded = true;

        _explosionEffect.PerformExplosion(transform.position, _explosionRadius, _explosionForce);

        BombExploded?.Invoke(this);
    }
}