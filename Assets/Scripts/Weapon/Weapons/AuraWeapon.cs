using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AuraWeapon : Weapon
{
    private Animator auraAnimator;

    private List<Enemy> enemyInRangeList;

    protected override void Awake()
    {
        base.Awake();

        enemyInRangeList = new List<Enemy>();
        auraAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        transform.localScale = new Vector3(runtimeDataSO.auraRadius, runtimeDataSO.auraRadius, transform.localScale.z);

        OnUpgradeApplied += Weapon_OnUpgradeApplied;
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
                enemy.TakeDamage(runtimeDataSO.damage);
                auraAnimator.Play("AuraAttack");
            }
        }
    }

    private void Weapon_OnUpgradeApplied()
    {
        Debug.Log("Transform.LocalScale = " + transform.localScale);
        transform.localScale = new Vector3(
            runtimeDataSO.auraRadius, 
            runtimeDataSO.auraRadius, 
            transform.localScale.z);
        Debug.Log("Transform.LocalScale = " + transform.localScale);
        Debug.Log("AuraWeapon.cs: RuntimeDataSO.AuraRadius = "  + runtimeDataSO.auraRadius);
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
