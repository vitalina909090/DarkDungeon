using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    private Health health;
    private Animator animator;
    private EnemyAI enemyAI;
    private Collider2D col;

    void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        col = GetComponent<Collider2D>();

        health.OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        if (enemyAI != null) enemyAI.enabled = false;
        if (col != null) col.enabled = false;
        if (animator != null) animator.SetTrigger("Die");
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= HandleDeath;
        }
    }
}