using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopUIManager : MonoBehaviour
{
    public GameObject shopUI;
    public Button firstSelectedButton;
    void Update()
    {
        if (shopUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }

    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f;
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

    public void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f; 
    }
}
