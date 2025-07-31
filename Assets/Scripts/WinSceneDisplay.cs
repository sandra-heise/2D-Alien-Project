using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinSceneDisplay : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text coinsText;
    public TMP_Text livesText;

    void Start()
    {
        timeText.text = "Time: " + FormatTime(GameManager.Instance.finalTime);
        coinsText.text = "Coins: " + GameManager.Instance.collectedCoins;
        livesText.text = "Used Lives: " + GameManager.Instance.livesUsed;
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene("Game"); 
    }
}
