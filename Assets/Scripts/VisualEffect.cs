using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class VisualEffect : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;
    private Coroutine _fadeCoroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        CreateFadeMaterial();
    }

    public void StartFadeEffect(Vector2 fadeTimeRange)
    {
        float fadeTime = Random.Range(fadeTimeRange.x, fadeTimeRange.y);

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeCoroutine(fadeTime));
    }

    public void ResetEffect()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        _material.color = Color.white;
    }

    private IEnumerator FadeCoroutine(float fadeTime)
    {
        float elapsewdTime = 0f;
        Color startColor = Color.black;
        Color endColor = new Color(0, 0, 0, 0);

        while (elapsewdTime < fadeTime)
        {
            elapsewdTime += Time.deltaTime;
            float time = elapsewdTime / fadeTime;
            _material.color = Color.Lerp(startColor, endColor, time);

            yield return null;
        }

        _material.color = endColor;
    }

    private void CreateFadeMaterial()
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

    private void OnDestroy()
    {
        if (_material != null)
            Destroy(_material);
    }
}