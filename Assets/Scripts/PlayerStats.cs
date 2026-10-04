using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Level progress")]
    [SerializeField] private int level = 1;
    [SerializeField] private int currentExp = 0;
    [SerializeField] private int expToNextLevel = 50;

    [Header("Base stats (level 1)")]
    [SerializeField] private int baseMaxHealth = 100;
    [SerializeField] private int baseDamage = 15;

    [Header("Growth per level")]
    [SerializeField] private int healthGrowthPerLevel = 20;
    [SerializeField] private int damageGrowthPerLevel = 5;
    [SerializeField] private float expRequirementGrowth = 1.3f;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int ExpToNextLevel => expToNextLevel;
    public int CurrentMaxHealth => baseMaxHealth + (level - 1) * healthGrowthPerLevel;
    public int CurrentDamage => baseDamage + (level - 1) * damageGrowthPerLevel;

    public event Action OnStatsChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();
        }

        OnStatsChanged?.Invoke();
    }

    void LevelUp()
    {
        level++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * expRequirementGrowth);
    }
}