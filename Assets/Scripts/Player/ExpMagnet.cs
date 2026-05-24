using UnityEngine;

public class ExpMagnet : MonoBehaviour
{
    [SerializeField] private float magnetRadius;
    [SerializeField] private float pullSpeed;
    [SerializeField] private LayerMask expLayer;

    private Collider2D[] hits = new Collider2D[64];
    private ContactFilter2D filter;

    private void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(expLayer);
        filter.useTriggers = true;
    }

    private void Update()
    {
        int count = Physics2D.OverlapCircle(transform.position, magnetRadius, filter, hits);
        
        for (int i = 0; i < count; i++)
        {
            hits[i].transform.position = Vector2.MoveTowards(
                hits[i].transform.position,
                transform.position,
                pullSpeed * Time.deltaTime
            );
        }
    }
}
