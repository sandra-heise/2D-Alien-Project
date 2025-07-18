using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isClimbing = false;
    private float climbSpeed = 3f;
    private float normalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (isClimbing)
        {
            rb.gravityScale = 0f;

            if (vertical != 0 || horizontal != 0)
            {
                rb.linearVelocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
            }
            else
            {
                // Stillstand erzwingen
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
