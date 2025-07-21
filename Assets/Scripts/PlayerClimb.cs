using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isClimbing = false;
    private float climbSpeed = 3f;
    private float normalGravity;
    private Animator animator;
    public float jumpForce = 10f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            rb.gravityScale = 0f;

            // Beim Springen klettern abbrechen
            if (Input.GetButtonDown("Jump"))
            {
                isClimbing = false;
                rb.gravityScale = normalGravity;
                animator.SetBool("isClimbing", false);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // ? Aktiver Sprung nach oben
            }
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
            rb.gravityScale = normalGravity;
        }
    }
}
