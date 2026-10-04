using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Visuals")]
    [SerializeField] private float spriteScale = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(horizontal, vertical);

        if (moveInput.magnitude > 1f)
        {
            moveInput = moveInput.normalized;
        }

        if (animator != null)
        {
            animator.SetBool("IsMoving", moveInput.magnitude > 0.1f);
        }

        if (horizontal > 0.01f) transform.localScale = new Vector3(spriteScale, spriteScale, 1f);
        else if (horizontal < -0.01f) transform.localScale = new Vector3(-spriteScale, spriteScale, 1f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}