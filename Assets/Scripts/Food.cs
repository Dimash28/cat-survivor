using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float healAmount;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Player.Instance.Heal(healAmount);
            
            Destroy(gameObject);
        }
    }
}
