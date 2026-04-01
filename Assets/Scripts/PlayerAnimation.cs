using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastDir;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();

        if (inputVector.x > 0)
        {
            animator.Play("WalkRight");
        }
        else if (inputVector.x < 0)
        {
            animator.Play("WalkLeft");
        }
        else if (inputVector.y > 0 && inputVector.x == 0)
        {
            animator.Play("WalkUp");
        }
        else if (inputVector.y < 0 && inputVector.x == 0)
        {
            animator.Play("WalkDown"); 
        }

        lastDir = inputVector;

        if(inputVector == Vector2.zero)
        {
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        if(lastDir.x > 0 && lastDir.y == 0)
        {
            animator.Play("IdleRight");
        }
        else if(lastDir.x < 0 && lastDir.y == 0)
        {
            animator.Play("IdleLeft");
        }
        else if(lastDir.x == 0 && lastDir.y > 0)
        {
            animator.Play("IdleUp");
        }
        else if(lastDir.x == 0 && lastDir.y < 0)
        {
            animator.Play("IdleDown");
        }
    }
}
