using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerShadowAnimator;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Vector2 lastInputVector;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetInputParameter();
    }

    private void SetInputParameter()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();

        if (inputVector != Vector2.zero)
        {
            playerAnimator.SetBool("IsWalking", true);
            playerShadowAnimator.SetBool("IsWalking", true);

            lastInputVector = inputVector;

            playerAnimator.SetFloat("InputX", inputVector.x);
            playerAnimator.SetFloat("InputY", inputVector.y);
            playerShadowAnimator.SetFloat("InputX", inputVector.x);
            playerShadowAnimator.SetFloat("InputY", inputVector.y);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
            playerShadowAnimator.SetBool("IsWalking", false);

            playerAnimator.SetFloat("LastInputX", lastInputVector.x);
            playerAnimator.SetFloat("LastInputY", lastInputVector.y);
            playerShadowAnimator.SetFloat("LastInputX", lastInputVector.x);
            playerShadowAnimator.SetFloat("LastInputY", lastInputVector.y);
        }
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
}
