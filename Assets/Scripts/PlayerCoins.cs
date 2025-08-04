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
        PlaySoundWithVolume(coinSound, transform.position, 0.7f);
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
    private void PlaySoundWithVolume(AudioClip clip, Vector3 position, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.Play();

        Destroy(tempGO, clip.length);
    }
}
