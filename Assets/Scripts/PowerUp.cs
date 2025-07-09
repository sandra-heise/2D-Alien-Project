using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerup : MonoBehaviour
{
    public Sprite greenSprite; // Default
    public Sprite blueSprite;  // blue

    public TextMeshProUGUI timerText; 

    private SpriteRenderer sr;
    private bool isPoweredUp = false;
    private float powerDuration = 60f;
    private float currentTime;
    private Diamond lastCollectedDiamond;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = greenSprite;

        if (timerText != null)
            timerText.text = "";
    }

    void Update()
    {
        if (isPoweredUp)
        {
            currentTime -= Time.deltaTime;

           if (timerText != null)
                timerText.text = Mathf.CeilToInt(currentTime).ToString();

            if (currentTime <= 0)
            {
                EndPowerUp();
            }
        }
    }

    void EndPowerUp()
    {
        isPoweredUp = false;
        sr.sprite = greenSprite;

        if (timerText != null)
            timerText.text = "";
    }

    public void ActivatePowerUp(Diamond diamond)
    {
        sr.sprite = blueSprite;
        isPoweredUp = true;
        currentTime = powerDuration;
        lastCollectedDiamond = diamond;

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    public void CancelPowerUp()
    {
        isPoweredUp = false;
        sr.sprite = greenSprite;

        if (timerText != null)
            timerText.text = "";

        if (lastCollectedDiamond != null)
        {
            lastCollectedDiamond.Respawn();
            lastCollectedDiamond = null; // einmaliger Respawn
        }

    }

}
