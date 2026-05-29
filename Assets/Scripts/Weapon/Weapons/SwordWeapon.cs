using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SwordWeapon : Weapon
{
    private Animator animator;
    private bool isAttacking = false;
    private Vector2 lastMoveDirection = Vector2.right;
    private BoxCollider2D swordCollider;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        swordCollider = GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
    }

    protected override void Update()
    {
        Vector2 input = GameInput.Instance.GetInputVectorNormalized();
        if (input != Vector2.zero)
            lastMoveDirection = input;

        base.Update();
    }

    protected override void Attack()
    {
        if (isAttacking) return;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(
            runtimeDataSO.projectileScale,
            runtimeDataSO.projectileScale, 
            0f);

        animator.Play("SwordAttack");

        yield return new WaitForSeconds(0.1f);

        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        swordCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAttacking) return;

        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(runtimeDataSO.damage);
        }
    }
}