
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadingManager : MonoBehaviour
{
    private float introDuration = 3f;
    private string mainSceneName = "MainMenu";

    private void Start()
    {
        Invoke(nameof(LoadMainScene), introDuration);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadMainScene();
        }
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
