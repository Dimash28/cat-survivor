using UnityEngine;

public class AuraWeapon : Weapon
{
    private GameObject auraVisual;
    private float tickTimer;

    protected override void Awake()
    {
        base.Awake();
        CreateAuraVisual();
    }

    private void CreateAuraVisual()
    {
        if (runtimeDataSO.AuraPrefab != null)
        {
            auraVisual = Instantiate(runtimeDataSO.AuraPrefab, transform);
        }
    }

    protected override void Update()
    {
        base.Update();

        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0f)
        {
            Attack();
            tickTimer = runtimeDataSO.TickRate;
        }
    }

    protected override void Attack()
    {
        
    }
}
