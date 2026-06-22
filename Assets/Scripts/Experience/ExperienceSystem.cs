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
    private int pendingLevelUps = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        level = 1;
        defaultExpAmountToNextLevel = 100;
        expAmountToNextLevelIncrement = 1.3f;

        currentExpAmountToNextLevel = defaultExpAmountToNextLevel;
    }

    public void AddExpToCurrentAmount(float expAmount)
    {
        currentExperienceAmount += expAmount;
        OnExpGained?.Invoke();

        while (currentExperienceAmount >= currentExpAmountToNextLevel)
        {
            currentExperienceAmount -= currentExpAmountToNextLevel;
            currentExpAmountToNextLevel *= expAmountToNextLevelIncrement;
            level++;
            pendingLevelUps++;
            OnCurrentExpAmountReset?.Invoke();
        }

        if (pendingLevelUps > 0 && !G.levelUpUI.IsShowing)
            TriggerNextLevelUp();
    }

    public void TriggerNextLevelUp()
    {
        if (pendingLevelUps <= 0) return;
        pendingLevelUps--;
        OnLevelUp?.Invoke();
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
