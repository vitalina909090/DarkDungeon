using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Patrolling, Chasing, Returning }

    [Header("Patrol settings")]
    [SerializeField] private float patrolRadius = 3f;
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float waitTimeAtPoint = 1.5f;

    [Header("Chase settings")]
    [SerializeField] private float detectionRadius = 4f;
    [SerializeField] private float loseTargetRadius = 6f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Level bounds (enemy cannot cross these)")]
    [SerializeField] private Vector2 boundsMin = new Vector2(-7.5f, -5.5f);
    [SerializeField] private Vector2 boundsMax = new Vector2(7.5f, 5.5f);

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 homePosition;
    private Vector2 currentPatrolTarget;
    private Transform player;
    private State currentState = State.Patrolling;
    private float waitTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        homePosition = rb.position;
        PickNewPatrolPoint();
    }

    void Update()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(rb.position, detectionRadius, playerLayer);

        if (playerHit != null)
        {
            player = playerHit.transform;
            currentState = State.Chasing;
        }
        else if (currentState == State.Chasing && player != null)
        {
            float distanceToPlayer = Vector2.Distance(rb.position, player.position);
            if (distanceToPlayer > loseTargetRadius)
            {
                currentState = State.Returning;
                player = null;
            }
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Returning:
                ReturnHome();
                break;
        }
    }

    void Patrol()
    {
        float distanceToTarget = Vector2.Distance(rb.position, currentPatrolTarget);

        if (distanceToTarget < 0.1f)
        {
            if (animator != null) animator.SetBool("IsMoving", false);

            waitTimer += Time.fixedDeltaTime;
            if (waitTimer >= waitTimeAtPoint)
            {
                PickNewPatrolPoint();
                waitTimer = 0f;
            }
            return;
        }

        Vector2 direction = (currentPatrolTarget - rb.position).normalized;
        MoveTowards(direction, patrolSpeed);
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        MoveTowards(direction, chaseSpeed);
    }

    void ReturnHome()
    {
        float distanceToHome = Vector2.Distance(rb.position, homePosition);

        if (distanceToHome < 0.1f)
        {
            currentState = State.Patrolling;
            PickNewPatrolPoint();
            return;
        }

        Vector2 direction = (homePosition - rb.position).normalized;
        MoveTowards(direction, patrolSpeed);
    }

    void MoveTowards(Vector2 direction, float speed)
    {
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, boundsMin.x, boundsMax.x);
        newPosition.y = Mathf.Clamp(newPosition.y, boundsMin.y, boundsMax.y);

        rb.MovePosition(newPosition);

        if (animator != null) animator.SetBool("IsMoving", true);
    }

    void PickNewPatrolPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
        Vector2 candidate = homePosition + randomOffset;

        candidate.x = Mathf.Clamp(candidate.x, boundsMin.x, boundsMax.x);
        candidate.y = Mathf.Clamp(candidate.y, boundsMin.y, boundsMax.y);

        currentPatrolTarget = candidate;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 center = Application.isPlaying ? (Vector3)homePosition : transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, patrolRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = new Color(1f, 0.5f, 0f);
        Gizmos.DrawWireSphere(transform.position, loseTargetRadius);

        Gizmos.color = Color.cyan;
        Vector3 size = new Vector3(boundsMax.x - boundsMin.x, boundsMax.y - boundsMin.y, 0f);
        Vector3 boundsCenter = new Vector3((boundsMin.x + boundsMax.x) / 2f, (boundsMin.y + boundsMax.y) / 2f, 0f);
        Gizmos.DrawWireCube(boundsCenter, size);
    }
}