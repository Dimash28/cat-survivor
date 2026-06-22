using Unity.VisualScripting;
using UnityEngine;

public class SecondSwordBehaviour : MonoBehaviour
{
    [SerializeField] private SwordWeapon mainSword;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!mainSword.IsAttacking()) return;

        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(mainSword.GetDamage());
        }
    }
}
