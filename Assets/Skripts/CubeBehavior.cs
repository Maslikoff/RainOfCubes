using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.blue;
    [SerializeField] private Color _touchedColor = Color.red;
    [SerializeField] private Vector2 _rangeLife = new Vector2(2, 5);

    private Renderer _renderer;
    private bool _hasTouchedPlatform = false;
    private float _lifeTime;
    private float _timeSinceTouch;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        ResetCube();
    }

    public void ResetCube()
    {
        _renderer.material.color = _initialColor;
        _hasTouchedPlatform = false;
        _timeSinceTouch = 0;
    }

    private void Update()
    {
        if (_hasTouchedPlatform) 
        { 
            _timeSinceTouch += Time.deltaTime;

            if(_timeSinceTouch >= _lifeTime)
                CubePool.Instance.ReturnCube(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") && _hasTouchedPlatform == false)
        {
            _hasTouchedPlatform = true;
            _renderer.material.color = _touchedColor;
            _lifeTime = Random.Range(_rangeLife.x, _rangeLife.y);
        }
    }
}
