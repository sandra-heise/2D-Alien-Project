using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class MainMenu : MonoBehaviour
{

    public string introSceneName = "IntroScene";
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

    public void PlayGame()
    {
        CheckpointManager.ResetCheckpoints();
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
       
        SceneManager.LoadScene(introSceneName);
       
    }

}