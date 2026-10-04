using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int damage = 15;
    [SerializeField] private float attackCooldown = 0.6f;
    [SerializeField] private LayerMask enemyLayer;

    private float cooldownTimer = 0f;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
    }

    void Attack()
    {
        if (animator != null) animator.SetTrigger("Attack");

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        if (hits.Length == 0) return;

        Collider2D nearest = hits[0];
        float nearestDistance = Vector2.Distance(attackPoint.position, nearest.transform.position);

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(attackPoint.position, hit.transform.position);
            if (distance < nearestDistance)
            {
                nearest = hit;
                nearestDistance = distance;
            }
        }

        Health enemyHealth = nearest.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}