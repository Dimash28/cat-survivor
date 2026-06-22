using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon
{
    [SerializeField] private GameObject secondSword;
    [SerializeField] private Animator mainSwordAnimator;
    [SerializeField] private Animator secondSwordAnimator;
    [SerializeField] private List<SoundSO> swordSoundList;   
    protected bool isAttacking = false;
    private Vector2 lastMoveDirection = Vector2.right;
    private BoxCollider2D swordCollider;
    private BoxCollider2D secondSwordCollider;

    private bool isSecondSwordActivated;

    protected override void Awake()
    {
        base.Awake();
        swordCollider = GetComponent<BoxCollider2D>();
        secondSwordCollider = secondSword.GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
        secondSwordCollider.enabled = false;
        secondSword.SetActive(false);
        isSecondSwordActivated = false;

        OnUpgradeApplied += ActivateSecondSword;
    }

    protected override void Update()
    {
        Vector2 input = G.input.GetInputVectorNormalized();
        if (input != Vector2.zero)
            lastMoveDirection = input;

        base.Update();
    }

    protected override void Attack()
    {
        if (isAttacking) return;
        StartCoroutine(AttackCoroutine());

        G.audio.Play(swordSoundList[Random.Range(0, swordSoundList.Count)]);
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(
            runtimeDataSO.projectileScale,
            runtimeDataSO.projectileScale, 
            1f);

        mainSwordAnimator.Play("SwordAttack");
        if(isSecondSwordActivated) secondSwordAnimator.Play("SwordAttack");
        yield return new WaitForSeconds(0.1f);

        swordCollider.enabled = true;
        if(isSecondSwordActivated) secondSwordCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        swordCollider.enabled = false;
        if(isSecondSwordActivated) secondSwordCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
    }

    private void ActivateSecondSword()
    {
        if(runtimeDataSO.projectileCount > 1) secondSword.SetActive(true);
        isSecondSwordActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAttacking) return;

        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(runtimeDataSO.damage);
        }
    }

    public float GetDamage()
    {
        return runtimeDataSO.damage;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}