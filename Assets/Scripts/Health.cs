using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float destroyDelay = 0f;
    private int currentHealth;

    public event Action OnDeath;
    public event Action OnHealthChanged;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke();

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Invoke(nameof(DestroySelf), destroyDelay);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke();
    }
}