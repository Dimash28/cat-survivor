using Unity.VisualScripting;
using UnityEngine;

public class KnifeWeapon : Weapon
{
    private Vector2 latestInputVector;

    private void Start()
    {
        latestInputVector = Vector2.right;
    }

    protected override void Update()
    {
        base.Update();

        Vector2 currentInput = GameInput.Instance.GetInputVectorNormalized();
        
        if(currentInput != Vector2.zero)
        {
            latestInputVector = currentInput;
        }
    }

    protected override void Attack()
    {
        if(runtimeDataSO == null) return;

        for (int i = 0; i < runtimeDataSO.projectileCount; i++)
        {
            float angleOffset = (i - (runtimeDataSO.projectileCount - 1) / 2f) * 15f;

            GameObject knife = Instantiate(runtimeDataSO.projectilePrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);

            Vector2 direction;

            direction = Quaternion.Euler(0, 0, angleOffset) * latestInputVector;

            Projectile proj = knife.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Setup(direction * runtimeDataSO.projectileSpeed, runtimeDataSO.damage);
            }
        }
    }
}
