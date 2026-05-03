using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AuraWeapon : Weapon
{
    private GameObject auraVisual;
    private Animator auraAnimator;
    private new CircleCollider2D collider2D;
    private float currentDamage;

    private List<Enemy> enemyInRangeList;

    protected override void Awake()
    {
        base.Awake();
        CreateAuraVisual();

        enemyInRangeList = new List<Enemy>();
    }

    private void Start()
    {
        currentDamage = runtimeDataSO.damage;
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void CreateAuraVisual()
    {
        if (runtimeDataSO.auraPrefab != null)
        {
            auraVisual = Instantiate(runtimeDataSO.auraPrefab, transform);
            auraAnimator = auraVisual.GetComponentInChildren<Animator>();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        foreach(Enemy enemy in enemyInRangeList.ToArray())
        {
            if(enemy != null)
            {
                enemy.TakeDamage(currentDamage);
                auraAnimator.Play("AuraAttack");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(!enemyInRangeList.Contains(enemy))
                enemyInRangeList.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyInRangeList.Remove(enemy);
        }
    }
}
