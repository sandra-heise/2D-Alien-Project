using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] private string winningSceneName = "End";
    [SerializeField] private LevelTimer levelTimer;

    private void Start()
    {

        if (levelTimer == null)
        {
            levelTimer = FindObjectOfType<LevelTimer>();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Absicherung: Falls levelTimer nie zugewiesen wurde
        if (levelTimer == null)
        {
            levelTimer = FindObjectOfType<LevelTimer>();
            if (levelTimer == null)
            {
                Debug.LogError("LevelTimer konnte im OnTriggerEnter2D nicht gefunden werden.");
                return;
            }
        }

        // Timer stoppen
        levelTimer.StopTimer();

        // Spielstatus speichern
        GameManager.Instance.finalTime = levelTimer.TimeElapsed;

        var coinTracker = FindObjectOfType<PlayerCoins>();
        if (coinTracker != null)
            GameManager.Instance.collectedCoins = coinTracker.GetCoins();
        else
            Debug.LogWarning("PlayerCoins konnte nicht gefunden werden.");

        GameManager.Instance.livesUsed = PlayerHealth.LivesUsed;

        SceneManager.LoadScene(winningSceneName);
    }

}
