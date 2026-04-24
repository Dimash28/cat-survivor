using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance {get; private set;}

    public event System.Action OnExpGained;
    public event System.Action OnCurrentExpAmountReset;
    public event System.Action OnLevelUp;

    private int level;
    private float currentExpAmountToNextLevel;
    private float defaultExpAmountToNextLevel;
    private float currentExperienceAmount;
    private float expAmountToNextLevelIncrement;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        level = 1;
        defaultExpAmountToNextLevel = 100;
        expAmountToNextLevelIncrement = 1.5f;

        currentExpAmountToNextLevel = defaultExpAmountToNextLevel;
    }

    public void AddExpToCurrentAmount(float expAmount)
    {
        currentExperienceAmount += expAmount;
        OnExpGained?.Invoke();
        Debug.Log($"Current Experience Amount: {currentExperienceAmount} out of {currentExpAmountToNextLevel}");

        if (currentExperienceAmount >= currentExpAmountToNextLevel)
        {
            level++;
            OnLevelUp?.Invoke();

            currentExpAmountToNextLevel *= expAmountToNextLevelIncrement;
            currentExperienceAmount = 0;
            OnCurrentExpAmountReset?.Invoke();

            Debug.Log("Current Level: " + level);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetCurrentExpAmount()
    {
        return currentExperienceAmount;
    }

    public float GetCurrentExpAmountToNextLevel()
    {
        return currentExpAmountToNextLevel;
    }
}
