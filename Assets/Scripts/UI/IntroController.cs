using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    private float introDuration = 12f; 
    private string mainSceneName = "Game";

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
