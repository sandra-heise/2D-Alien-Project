using UnityEngine;
using TMPro;
using System.Collections;
public class PlayerPowerup : MonoBehaviour
{
    public bool CanSwim { get; private set; } = false;
    public bool CanHighJump { get; private set; } = false;
    public bool IsInvisible { get; private set; } = false;
    public bool CanShoot { get; private set; } = false;
    

    private SpriteRenderer spriteRenderer;
    private Coroutine currentPowerupRoutine;
    public TMP_Text timeText;
    public Sprite defaultSprite; // grün
    public Sprite blueSprite;    // für Schwimmen
    public Sprite redSprite;     // für hohen Sprung
    public Sprite invisibleSprite;     // für unsichtbarkeit
    public Sprite shootSprite;     // für schiessen
    private Animator animator;

    public Transform firePoint; 
    public float shootCooldown = 0.5f;

    public GameObject LaserPrefab;
    public GameObject cannonVisual;

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
    void Update()
    {
        if (CanShoot && Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    public void ActivatePowerUp(PowerUpType type, float duration)
    {
        if (currentPowerupRoutine != null)
        {
            StopCoroutine(currentPowerupRoutine);
        }

        StartCoroutine(HighlightTimeText());

        switch (type)
        {
            case PowerUpType.Swim:
                CanSwim = true;
                spriteRenderer.sprite = blueSprite;
                if (BackgroundMusicManager.Instance != null)
                {
                    BackgroundMusicManager.Instance.PlayPowerUpMusic();
                }
                if (animator != null)
                {
                    animator.SetBool("isBlue", true);
                }
                break;

            case PowerUpType.HighJump:
                CanHighJump = true;
                spriteRenderer.sprite = redSprite;
                if (BackgroundMusicManager.Instance != null)
                {
                    BackgroundMusicManager.Instance.PlayPowerUpMusic();
                }
                if (animator != null)
                {
                    animator.SetBool("isRed", true);
                }
                break;

            case PowerUpType.Invisible:
                IsInvisible = true;
                spriteRenderer.sprite = invisibleSprite;
                if (animator != null)
                {
                    animator.SetBool("isInvisible", true);
                }
                break;

            case PowerUpType.Shoot:
                CanShoot = true;
                spriteRenderer.sprite = shootSprite;
                if (animator != null)
                {
                    animator.SetBool("isYellow", true);
                }
                if (cannonVisual != null)
                {
                    cannonVisual.SetActive(true);
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
        IsInvisible = false;
        CanShoot = false;
        spriteRenderer.sprite = defaultSprite;

        if (animator != null)
        {
            animator.SetBool("isBlue", false);
            animator.SetBool("isRed", false);
            animator.SetBool("isYellow", false);
            animator.SetBool("isInvisible", false);
        }

        if (timeText != null)
        {
            timeText.gameObject.SetActive(false);
        }
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.StopPowerUpMusic();
        }
        if (cannonVisual != null)
        {
            cannonVisual.SetActive(false);
        }
    }
    private IEnumerator HighlightTimeText()
    {
        float startSize = 50f;
        float endSize = 36f;
        float duration = 1.5f;
        float elapsed = 0f;

        timeText.fontSize = startSize;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            timeText.fontSize = Mathf.Lerp(startSize, endSize, t);
            yield return null;
        }

        timeText.fontSize = endSize;
    }

    private void Shoot()
    {
        GameObject laser = Instantiate(LaserPrefab, firePoint.position, Quaternion.identity);
        Laser projScript = laser.GetComponent<Laser>();

        if (transform.localScale.x < 0) // Spieler schaut nach links?
        {
            projScript.SetDirection(Vector2.left);
            laser.transform.localScale = new Vector3(-1, 1, 1); // ggf. spiegeln
        }
        else
        {
            projScript.SetDirection(Vector2.right);
        }
    }

}
