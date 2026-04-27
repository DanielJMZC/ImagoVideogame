using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Vector3 originalPos;
    
    [Header("Arrastra aquí tu Canvas o Panel para que también tiemble")]
    public RectTransform uiToShake;
    private Vector2 originalUiPos;

    private void Awake()
    {
        instance = this;
        originalPos = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (uiToShake != null)
        {
            originalUiPos = uiToShake.anchoredPosition;
        }
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Tiembla la cámara (para World Space)
            transform.localPosition = originalPos + new Vector3(x, y, 0);

            // Tiembla la UI (para Screen Space). Multiplicamos por 10 porque 5 unidades en el mundo es mucho, pero 5 píxeles en UI es casi invisible.
            if (uiToShake != null)
            {
                uiToShake.anchoredPosition = originalUiPos + new Vector2(x * 10f, y * 10f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        if (uiToShake != null)
        {
            uiToShake.anchoredPosition = originalUiPos;
        }
    }
}