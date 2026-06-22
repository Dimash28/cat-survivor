using UnityEngine;

public class ExpBehaviour : MonoBehaviour
{
    [SerializeField] private SoundSO expSound;
    private ExpData expData;

    private void Awake()
    {
        expData = GetComponent<ExpData>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            G.experience.AddExpToCurrentAmount(expData.GetExperienceAmount());
            
            G.audio.Play(expSound);
            Destroy(gameObject);
        }
    }
}
