using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void PlayHitEffect()
    {
        originalPosition = transform.position;
        StartCoroutine(HitEffectCoroutine());
    }

    private IEnumerator HitEffectCoroutine()
    {
        float duration = 0.15f;
        float shakeAmount = 0.08f;
        Color originalColor = spriteRenderer.color;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

            spriteRenderer.color = Color.red;

            yield return null;
        }

        transform.position = originalPosition;
        spriteRenderer.color = originalColor;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        spriteRenderer.color = Color.white;
        transform.localPosition = Vector3.zero;
    }
}
