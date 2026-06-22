using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerShadowAnimator;
    private Animator playerAnimator;
    private Vector2 lastInputVector;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetInputParameter();
    }

    private void SetInputParameter()
    {
        Vector2 inputVector = G.input.GetInputVectorNormalized();

        if (inputVector != Vector2.zero)
        {
            lastInputVector = inputVector;

            SetAnimatorParameters(true, inputVector.x, inputVector.y, lastInputVector.x, lastInputVector.y);
        }
        else
        {
            SetAnimatorParameters(false, inputVector.x, inputVector.y, lastInputVector.x, lastInputVector.y);
        }
    }

    private void SetAnimatorParameters(bool isWalking, float x, float y, float lastX, float lastY)
    {
        foreach (var animator in new[] { playerAnimator, playerShadowAnimator })
        {
            animator.SetBool("IsWalking", isWalking);
            animator.SetFloat("InputX", x);
            animator.SetFloat("InputY", y);
            animator.SetFloat("LastInputX", lastX);
            animator.SetFloat("LastInputY", lastY);
        }
    }
}
