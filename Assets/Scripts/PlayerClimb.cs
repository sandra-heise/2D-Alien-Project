using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isClimbing = false;
    private float climbSpeed = 3f;
    private float normalGravity;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            animator.SetBool("isClimbing", true);
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            animator.SetBool("isClimbing", false);
            isClimbing = false;
        }
    }
}
