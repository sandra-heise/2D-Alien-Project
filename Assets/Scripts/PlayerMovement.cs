using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("UI")]
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI coinText;

    [Header("Umbrella")]
    public Transform umbrellaAttachPoint;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerPowerup powerup;

    private bool isGrounded;
    private bool hasUmbrella = false;
    private GameObject currentUmbrella;

    private int lives = 3;
    private int coinCount = 0;
    private Vector2 startPosition = new Vector2(36f, 16f);//new Vector2(-8f, 0f);
    private int waterTriggerCount = 0;
    private bool isInWater => waterTriggerCount > 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerup = GetComponent<PlayerPowerup>();
        animator = GetComponent<Animator>();
        isGrounded = false;
        transform.position = startPosition;
        UpdateCoinUI();
    }

    void Update()
    {
        if (transform.position.y < -30f)
        {
            LoseLife();
            return;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (transform.position.x <= -8f && moveInput < 0)
            moveInput = 0f;

        HandleMovement(moveInput);
        HandleAnimation(moveInput);
        HandleDirection(moveInput);
    }

    private void HandleMovement(float moveInput)
    {
        if (isInWater && powerup?.IsPowered == true)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, verticalInput * moveSpeed);
        }
        else if (hasUmbrella)
        {
            rb.gravityScale = 0.5f;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, Mathf.Max(rb.linearVelocity.y, -1f));
        }
        else
        {
            rb.gravityScale = 3f;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    private void HandleAnimation(float moveInput)
    {
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0f);
        bool isPowered = powerup?.IsPowered == true;
        animator.SetBool("isPowered", isPowered);
        animator.SetBool("isSwimming", isInWater && isPowered);
    }

    private void HandleDirection(float moveInput)
    {
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = true;

            if (hasUmbrella)
            {
                hasUmbrella = false;
                if (currentUmbrella != null)
                {
                    currentUmbrella.transform.SetParent(null);
                    Destroy(currentUmbrella);
                    currentUmbrella = null;
                }
                rb.gravityScale = 3f;
            }
        }

        if (collision.collider.CompareTag("bridge") || collision.collider.CompareTag("MovingPlatform"))
            transform.SetParent(collision.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("bridge") || collision.collider.CompareTag("MovingPlatform"))
            transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "coin":
                coinCount++;
                UpdateCoinUI();
                Destroy(collision.gameObject);
                break;

            case "water":
                waterTriggerCount++;
                break;

            case "Spike":
                LoseLife();
                break;

            case "key":
                var key = collision.GetComponent<KeyFollower>();
                key?.Collect(transform);
                break;

            case "umbrella":
                if (!hasUmbrella)
                {
                    hasUmbrella = true;
                    currentUmbrella = collision.gameObject;
                    currentUmbrella.transform.SetParent(umbrellaAttachPoint);
                    currentUmbrella.transform.localPosition = Vector3.zero;
                    currentUmbrella.transform.localRotation = Quaternion.identity;

                    rb.gravityScale = 0.3f;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -1f);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            waterTriggerCount = Mathf.Max(0, waterTriggerCount - 1);
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = ": " + coinCount;
    }

    private void UpdateLifeUI()
    {
        if (lifeText != null)
            lifeText.text = "x " + lives;
    }

    private void LoseLife()
    {
        lives--;
        powerup?.CancelPowerUp();
        UpdateLifeUI();

        if (lives > 0)
        {
            transform.position = CheckpointManager.currentCheckpointPosition;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            CheckpointManager.ResetCheckpoints();
            SceneManager.LoadScene("GameOver");
        }

        FindObjectOfType<LeverMechanism>()?.ResetMechanism();

        var key = FindObjectOfType<KeyFollower>();
        if (key?.IsCollected() == true)
        {
            key.ResetKey();
        }
    }
}
