using UnityEngine;

public class ExpBehaviour : MonoBehaviour
{
    private ExpData expData;

    private void Awake()
    {
        expData = GetComponent<ExpData>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExperienceSystem.Instance.AddExpToCurrentAmount(expData.GetExperienceAmount());
            
            Destroy(gameObject);
        }
    }
}
