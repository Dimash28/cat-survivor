using UnityEngine;

public class ExpData : MonoBehaviour
{
    [SerializeField] private float experienceAmount;

    public float GetExperienceAmount()
    {
        return experienceAmount;
    }
}
