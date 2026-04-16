using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastInputVector;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool("IsWalking", true);

            lastInputVector = inputVector;

            animator.SetFloat("InputX", inputVector.x);
            animator.SetFloat("InputY", inputVector.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);

            animator.SetFloat("LastInputX", lastInputVector.x);
            animator.SetFloat("LastInputY", lastInputVector.y);
        }
    }
}
