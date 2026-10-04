using UnityEngine;

public class BossController : MonoBehaviour
{
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += HandleBossDeath;
    }

    void HandleBossDeath()
    {
        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.ShowWinScreen();
        }
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= HandleBossDeath;
        }
    }
}