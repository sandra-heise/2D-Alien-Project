using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    

    public TextMeshProUGUI coinText;
    public Transform umbrellaAttachPoint;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerPowerup powerup;
    private PlayerHealth playerHealth;
    private GameObject currentUmbrella;
    private MovingPlatform currentMovingPlatform;
    private BridgeMover currentBridgeMover;


    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool hasUmbrella = false;   
    private int coinCount = 0;
    private Vector2 startPosition = new Vector2(83f, 0f); // new Vector2(58f, 0f); // new Vector2(-8f, 0f);//new Vector2(14f, 0f); 
    private int waterTriggerCount = 0;
    private bool IsInWater => waterTriggerCount > 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerup = GetComponent<PlayerPowerup>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        isGrounded = false;
        transform.position = startPosition;
        UpdateCoinUI();
    }

    void Update()
    {
        if (transform.position.y < -30f)
        {
            StartCoroutine(playerHealth.LoseLife());
            return;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (transform.position.x <= -8f && moveInput < 0)
            moveInput = 0f;

        HandleMovement(moveInput);
        HandleAnimation(moveInput);
        HandleDirection(moveInput);
        if (currentMovingPlatform != null)
        {
            transform.position += currentMovingPlatform.PlatformVelocity;
        }

        if (currentBridgeMover != null)
        {
            transform.position += currentBridgeMover.PlatformVelocity;
        }
    }

    private void HandleMovement(float moveInput)
    {
        if (IsInWater && powerup?.CanSwim == true)
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
                float finalJumpForce = jumpForce;

                if (powerup != null && powerup.CanHighJump)
                {
                    finalJumpForce *= 1.4f; 
                }

                rb.AddForce(Vector2.up * finalJumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    private void HandleAnimation(float moveInput)
    {
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0f);
        bool isPowered = powerup?.CanSwim == true;
        animator.SetBool("isSwimming", IsInWater && isPowered);
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
        if (collision.collider.CompareTag("ground") || collision.collider.CompareTag("MovingPlatform")
        || collision.collider.CompareTag("bridge")) 
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

        if (collision.collider.CompareTag("MovingPlatform"))
        {
            currentMovingPlatform = collision.collider.GetComponent<MovingPlatform>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            currentMovingPlatform = null;
            isGrounded = false;
        }

        if (collision.collider.CompareTag("bridge"))
        {
            isGrounded = false;
        }
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

            case "heart":
                playerHealth.lives++;
                playerHealth.UpdateLifeUI();
                Destroy(collision.gameObject);
                break;

            case "water":
                waterTriggerCount++;
                break;

            case "Spike":
                StartCoroutine(playerHealth.LoseLife());
                break;

            case "saw":
                StartCoroutine(playerHealth.LoseLife());
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

            case "BridgeTrigger":
                BridgeTrigger trigger = collision.GetComponent<BridgeTrigger>();
                if (trigger != null)
                {
                    currentBridgeMover = trigger.bridgeMover;
                    UnityEngine.Debug.Log("Entered BridgeTrigger");
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
        if (collision.CompareTag("BridgeTrigger"))
        {
            currentBridgeMover = null;
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = ": " + coinCount;
    }    
}
