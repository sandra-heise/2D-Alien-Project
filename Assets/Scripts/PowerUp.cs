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
    public Sprite defaultSprite; // grün
    public Sprite blueSprite;    // für Schwimmen
    public Sprite redSprite;     // für hohen Sprung
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (timeText != null)
        {
            timeText.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = defaultSprite;
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
                spriteRenderer.sprite = blueSprite;
                if (animator != null)
                {
                    animator.SetBool("isBlue", true);
                }
                break;

            case PowerUpType.HighJump:
                CanHighJump = true;
                spriteRenderer.sprite = redSprite;
                if (animator != null)
                {
                    animator.SetBool("isRed", true);
                }
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
    }
    public void CancelPowerUp()
    {
        CanSwim = false;
        CanHighJump = false;
        spriteRenderer.sprite = defaultSprite;

        if (animator != null)
        {
            animator.SetBool("isBlue", false);
            animator.SetBool("isRed", false);
        }

        if (timeText != null)
        {
            timeText.gameObject.SetActive(false);
        }
    }
 }
