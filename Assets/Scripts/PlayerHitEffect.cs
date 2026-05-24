using System.Collections;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void PlayHitEffect()
    {
        StartCoroutine(HitEffectCoroutine());
    }

    private IEnumerator HitEffectCoroutine()
    {
        float duration = 0.15f;
        float shakeAmount = 0.08f;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            spriteTransform.localPosition = Vector3.zero + new Vector3(offsetX, offsetY, 0);

            spriteRenderer.color = Color.red;

            yield return null;
        }

        spriteTransform.localPosition = Vector3.zero;
        spriteRenderer.color = Color.white;
    }
}
