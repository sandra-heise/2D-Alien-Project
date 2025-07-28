using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopUIManager : MonoBehaviour
{
    public GameObject shopUI;
    public Button firstSelectedButton;
    public Button greenDiamondButton;
    public Button yellowDiamondButton;
  
    public int diamondCost = 20;

    public PlayerCoins playerCoins;
    public GameObject notEnoughCoinsPanel;
    public Button notEnoughCoinsOkButton;

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
    private IEnumerator SelectInfoButtonNextFrame()
    {
        yield return null;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(notEnoughCoinsOkButton.gameObject);
        }
    }


    public void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f; 
    }
    public void BuyGreenDiamond()
    {
        if (playerCoins.GetCoins() >= diamondCost)
        {
            playerCoins.SpendCoins(diamondCost);
            Debug.Log("Grüner Diamant gekauft!");
            // z. B. powerupManager.EnableInvisibility();
            CloseShop();
        }
        else
        {
            ShowNotEnoughCoinsMessage();
        }
    }

    public void BuyYellowDiamond()
    {
        if (playerCoins.GetCoins() >= diamondCost)
        {
            playerCoins.SpendCoins(diamondCost);
            Debug.Log("Gelber Diamant gekauft!");
            // z. B. powerupManager.EnableShooting();
            CloseShop();
        }
        else
        {
            ShowNotEnoughCoinsMessage();
            
        }
    }
    private void ShowNotEnoughCoinsMessage()
    {
       
        notEnoughCoinsPanel.SetActive(true);
        StartCoroutine(SelectInfoButtonNextFrame());

    }
    public void CloseInfoPanel()
    {
        shopUI.SetActive(false);
        notEnoughCoinsPanel.SetActive(false);
        Time.timeScale = 1f; 
    }



}
