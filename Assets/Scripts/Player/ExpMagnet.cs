using UnityEngine;

public class ExpMagnet : MonoBehaviour
{
    [SerializeField] private float magnetRadius;
    [SerializeField] private float pullSpeed;
    [SerializeField] private LayerMask expLayer;

    private void Update()
    {
        Vector2 point = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hits = Physics2D.OverlapCircleAll(point, magnetRadius, expLayer);

        foreach (var hit in hits)
        {
            Transform exp = hit.transform;

            exp.position = Vector2.MoveTowards(
                exp.position, 
                transform.position, 
                pullSpeed * Time.deltaTime
                );
        }
    }
}
