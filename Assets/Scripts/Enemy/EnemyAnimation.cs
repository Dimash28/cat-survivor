using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private Animator animator;

    private void Start() 
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        Vector2 direction = enemyMovement.GetDirectionToPlayer();

        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }
}
