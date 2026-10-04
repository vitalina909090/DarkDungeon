using UnityEngine;

public class PlayerStatsApplier : MonoBehaviour
{
    void Start()
    {
        if (PlayerStats.Instance == null) return;

        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.SetMaxHealth(PlayerStats.Instance.CurrentMaxHealth);
        }

        PlayerAttack attack = GetComponent<PlayerAttack>();
        if (attack != null)
        {
            attack.SetDamage(PlayerStats.Instance.CurrentDamage);
        }
    }
}