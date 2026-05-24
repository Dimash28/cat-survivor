using UnityEngine;

public class EnemyFlipSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shadowSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    private void Update()
    {
        bool flip = enemyMovement.GetDirectionToPlayer().x < 0;
        spriteRenderer.flipX = flip;
        shadowSpriteRenderer.flipX = spriteRenderer.flipX == true;
        
    }
}