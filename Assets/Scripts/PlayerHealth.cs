using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public TextMeshProUGUI lifeText;
    private Rigidbody2D rb;
    private PlayerPowerup powerup;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerup = GetComponent<PlayerPowerup>();
    }

    public void LoseLife()
    {
        Debug.Log("Lose Life");
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
    private void UpdateLifeUI()
    {
        if (lifeText != null)
            lifeText.text = "x " + lives;
    }
}
