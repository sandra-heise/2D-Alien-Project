using TMPro;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount = 0;
    public AudioClip coinSound;

    public void AddCoin()
    {
        coinCount ++;
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        UpdateCoinUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            UpdateCoinUI();
            return true;
        }
        return false;
    }

    public int GetCoins()
    {
        return coinCount;
    }

    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = ": " + coinCount;
    }
}
