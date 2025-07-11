using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    private bool isGrounded;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI coinText;
    private int lives = 3;
    private int coinCount = 0;
    private Vector2 startPosition = new Vector2(-8f, 0f);
    private int waterTriggerCount = 0;
    private bool isInWater => waterTriggerCount > 0;
    private PlayerPowerup powerup;

    private bool hasUmbrella = false;
    public Transform umbrellaAttachPoint;
    private GameObject currentUmbrella;

    private Animator animator;


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
        // Pruefe, ob Spieler zu tief gefallen ist
        if (transform.position.y < -30f)
        {
            LoseLife();
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        // Linke Begrenzung
        if (transform.position.x <= -8f && moveInput < 0)
        {
            moveInput = 0f;
        }

  
        if (isInWater && powerup != null && powerup.IsPowered)
        {
            // Spieler schwimmt frei (Pfeiltasten in alle Richtungen)
            float verticalInput = Input.GetAxisRaw("Vertical");

            rb.gravityScale = 0f; // Kein Sinken
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, verticalInput * moveSpeed);
        }
        else if (hasUmbrella)
        {
            rb.gravityScale = 0.5f;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, Mathf.Max(rb.linearVelocity.y, -1f));
        }
        else
        {
            // laufen und springen
            rb.gravityScale = 3f;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            // Springen
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        // Animationen setzen
        bool walking = Mathf.Abs(moveInput) > 0f;
        animator.SetBool("isWalking", walking);

        bool isPowered = powerup != null && powerup.IsPowered;
        animator.SetBool("isPowered", isPowered);

        bool isSwimming = isInWater && isPowered;
        animator.SetBool("isSwimming", isSwimming);

        // Figur drehen: nach rechts (normal) oder links (gespiegelt)
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  // schaut nach rechts
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // schaut nach links (gespiegelt)
        }

    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = true;
        }
        if (collision.collider.CompareTag("bridge"))
        {
            transform.SetParent(collision.transform);
        }
        if (collision.collider.CompareTag("ground") && hasUmbrella)
        {
            hasUmbrella = false;

            if (currentUmbrella != null)
            {
                currentUmbrella.transform.SetParent(null);
                Destroy(currentUmbrella); // oder: currentUmbrella.SetActive(false);
                currentUmbrella = null;
            }

            rb.gravityScale = 3f; // Urspr�ngliche Schwerkraft wiederherstellen
            UnityEngine.Debug.Log("Regenschirm entfernt beim Bodenkontakt.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("bridge"))
        {
            transform.SetParent(null);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            coinCount++;
            UpdateCoinUI();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("water"))
        {
            waterTriggerCount++;
        }
        if (collision.CompareTag("Spike"))
        {
            LoseLife();
        }
        if (collision.CompareTag("key"))
        {
            KeyFollower key = collision.GetComponent<KeyFollower>();
            if (key != null)
            {
                key.Collect(transform);
            }
        }


        if (collision.CompareTag("umbrella") && !hasUmbrella)
        {

            hasUmbrella = true;
            currentUmbrella = collision.gameObject;

            // Regenschirm an Kopf heften
            currentUmbrella.transform.SetParent(umbrellaAttachPoint);
            currentUmbrella.transform.localPosition = Vector3.zero;

            // Gravitation verringern
            rb.gravityScale = 0.3f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -1f);

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            waterTriggerCount--;
            if (waterTriggerCount < 0) waterTriggerCount = 0;
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = ": " + coinCount.ToString();
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

        PlayerPowerup power = GetComponent<PlayerPowerup>();
        if (power != null)
        {
            power.CancelPowerUp();
        }


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

        LeverMechanism lever = FindObjectOfType<LeverMechanism>();
        if (lever != null)
        {
            lever.ResetMechanism();
        }

        KeyFollower key = FindObjectOfType<KeyFollower>();
        if (key != null && key.IsCollected())
        {
            key.ResetKey();
        }

    }


}
