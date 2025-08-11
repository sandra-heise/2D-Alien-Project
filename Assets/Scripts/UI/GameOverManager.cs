using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject firstSelectedButton;
    private void Start()
    {
        StartCoroutine(SelectButtonNextFrame());

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
