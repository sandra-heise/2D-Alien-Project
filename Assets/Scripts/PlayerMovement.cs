using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    private bool isGrounded;
    public TextMeshProUGUI lifeText;
    public int maxLives = 99;         

    private int lives = 3;
    private Vector2 startPosition = new Vector2(-8f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = false;
        transform.position = startPosition;
    }

    void Update()
    {
        // Pr�fe, ob Spieler zu tief gefallen ist
        if (transform.position.y < -20f)
        {
            LoseLife();
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        // Linke Begrenzung
        if (transform.position.x <= -8f && moveInput < 0)
        {
            moveInput = 0f;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void UpdateLifeUI()
    {
        if (lifeText != null)
        {
            lifeText.text = "x " + lives.ToString();
        }
    }

    private void LoseLife()
    {
        lives--;

        UpdateLifeUI();

        if (lives > 0)
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
