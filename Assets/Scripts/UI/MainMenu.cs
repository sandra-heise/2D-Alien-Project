using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string introSceneName = "IntroScene";
    public string gameSceneName = "Game";
    public void PlayGame()
    {
        CheckpointManager.ResetCheckpoints();
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        if (PlayerPrefs.GetInt("IntroShown", 0) == 1)
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            SceneManager.LoadScene(introSceneName);
        }
    }

}