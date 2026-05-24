using System;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance {get; private set;}

    public event Action OnExpGained;
    public event Action OnCurrentExpAmountReset;
    public event Action OnLevelUp;

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
        expAmountToNextLevelIncrement = 1.1f;

        currentExpAmountToNextLevel = defaultExpAmountToNextLevel;
    }

    public void AddExpToCurrentAmount(float expAmount)
    {
        currentExperienceAmount += expAmount;
        OnExpGained?.Invoke();

        if (currentExperienceAmount >= currentExpAmountToNextLevel)
        {
            level++;
            OnLevelUp?.Invoke();

            currentExpAmountToNextLevel *= expAmountToNextLevelIncrement;
            currentExperienceAmount = 0;
            OnCurrentExpAmountReset?.Invoke();
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
