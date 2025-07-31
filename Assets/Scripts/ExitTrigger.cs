using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public string winningSceneName = "End";
    public LevelTimer levelTimer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelTimer.StopTimer();
            SceneManager.LoadScene(winningSceneName);
        }
    }
}
