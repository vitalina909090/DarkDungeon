using UnityEngine;

public class EnemyExperience : MonoBehaviour
{
    [SerializeField] private int experienceReward = 20;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += GiveExperience;
    }

    void GiveExperience()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.AddExperience(experienceReward);
        }
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= GiveExperience;
        }
    }
}