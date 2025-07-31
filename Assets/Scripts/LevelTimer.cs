using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float totalTime = 600f;
    public TMP_Text timerText;

    private float currentTime;
    private bool isRunning = true;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            HandleTimeOut();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void HandleTimeOut()
    {
       SceneManager.LoadScene("GameOver");
    }

    public void StopTimer()
    {
        isRunning = false;
    }
    public float TimeElapsed
    {
        get { return totalTime - currentTime; }
    }
}
