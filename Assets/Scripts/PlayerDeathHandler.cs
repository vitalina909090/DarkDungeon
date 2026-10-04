using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private Health health;
    private Animator animator;
    private PlayerMovement movement;
    private PlayerAttack attack;

    void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        health.OnDeath += HandlePlayerDeath;
    }

    void HandlePlayerDeath()
    {
        if (movement != null) movement.enabled = false;
        if (attack != null) attack.enabled = false;
        if (animator != null) animator.SetTrigger("Die");

        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.ShowLoseScreen();
        }
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= HandlePlayerDeath;
        }
    }
}