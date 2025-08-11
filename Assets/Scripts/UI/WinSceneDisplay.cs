using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinSceneDisplay : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text coinsText;
    public TMP_Text livesText;
    public GameObject firstSelectedButton;

    void Start()
    {
        StartCoroutine(SelectButtonNextFrame());
        timeText.text = "Time: " + FormatTime(GameManager.Instance.finalTime);
        coinsText.text = "Coins: " + GameManager.Instance.collectedCoins;
        livesText.text = "Used Lives: " + GameManager.Instance.livesUsed;
    }
    private IEnumerator SelectButtonNextFrame()
    {
        if (EventSystem.current == null)
        {
            yield break;
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    public void ReplayLevel()
    {
        CheckpointManager.ResetCheckpoints();

        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("Game"); 
    }
}
