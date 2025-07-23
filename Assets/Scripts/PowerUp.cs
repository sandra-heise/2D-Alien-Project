using UnityEngine;
using TMPro;
using System.Collections;
public class PlayerPowerup : MonoBehaviour
{
    public bool CanSwim { get; private set; } = false;
    public bool CanHighJump { get; private set; } = false;

    private SpriteRenderer spriteRenderer;
    private Coroutine currentPowerupRoutine;
    public TMP_Text timeText;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (timeText != null)
        {
            timeText.gameObject.SetActive(false);
        }
    }

    public void ActivatePowerUp(PowerUpType type, float duration)
    {
        if (currentPowerupRoutine != null)
        {
            StopCoroutine(currentPowerupRoutine);
        }

        switch (type)
        {
            case PowerUpType.Swim:
                CanSwim = true;
                spriteRenderer.color = Color.blue;
                break;

            case PowerUpType.HighJump:
                CanHighJump = true;
                spriteRenderer.color = Color.red;
                break;
        }
        currentPowerupRoutine = StartCoroutine(PowerUpTimer(type, duration));
    }

    private IEnumerator PowerUpTimer(PowerUpType type, float duration)
    {
        float timer = duration;

        if (timeText != null)
        {
            timeText.gameObject.SetActive(true);
        }

        while (timer > 0f)
        {
            if (timeText != null)
            {
                timeText.text = $"{Mathf.Ceil(timer)}";
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        CancelPowerUp();

        if (timeText != null)
        {
            timeText.gameObject.SetActive(false);
        }
    }
    public void CancelPowerUp()
    {
        CanSwim = false;
        CanHighJump = false;
        spriteRenderer.color = Color.green;
    }
 }
