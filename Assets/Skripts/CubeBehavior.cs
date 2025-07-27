using System.Collections;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.blue;
    [SerializeField] private Color _touchedColor = Color.red;
    [SerializeField] private Vector2 _rangeLife = new Vector2(2, 5);

    private Renderer _renderer;
    private bool _hasTouchedPlatform = false;
    private CubePool _cubePool;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        ResetCube();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            _hasTouchedPlatform = true;
            _renderer.material.color = _touchedColor;

            StartCoroutine(CountdownToReturn());
        }
    }

    public void SetPool(CubePool pool)
    {
        _cubePool = pool;
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

        _cubePool.ReturnCube(this);
    }
}
