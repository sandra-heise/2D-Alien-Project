using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 10;
    public TextMeshProUGUI lifeText;
    private Rigidbody2D rb;
    private PlayerPowerup powerup;
    private bool isDying = false;
    [SerializeField] private LavaController lavaController;
    public static int LivesUsed { get; private set; } = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerup = GetComponent<PlayerPowerup>();
        LivesUsed = 0;
    }

    [System.Obsolete]
    public IEnumerator LoseLife()
    {
        if (isDying) yield break;
        isDying = true;
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false; 

        // BLINKEN ALS STERBEANIMATION
        yield return StartCoroutine(FlashRoutine(1f)); 

        GetComponent<Collider2D>().enabled = true;

        lives--;
        LivesUsed++;
        powerup?.CancelPowerUp();
        UpdateLifeUI();
        lavaController.ResetLava();

        if (Diamond.lastCollectedDiamond != null)
        {
            Diamond.lastCollectedDiamond.Respawn();
            Diamond.lastCollectedDiamond = null;
        }

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
        yield return new WaitForSeconds(0.2f);
        isDying = false;
       
    }
    public void UpdateLifeUI()
    {
        if (lifeText != null)
            lifeText.text = ": " + lives;
    }
    private IEnumerator FlashRoutine(float duration)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        float elapsed = 0f;
        float flashSpeed = 0.1f;

        while (elapsed < duration)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(flashSpeed);
            sr.enabled = true;
            yield return new WaitForSeconds(flashSpeed);
            elapsed += flashSpeed * 2f;
        }
    }

}
