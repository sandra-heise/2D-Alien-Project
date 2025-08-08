using System.Collections;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float leftX = 101f;
    public float rightX = 110f;
    public float moveSpeed = 2f;

    [Header("Pause Movement")]
    public float pauseDownDistance = 2.8f;
    public float verticalMoveSpeed = 3f;
    public float waitAtBottom = 0.5f;
    public float waitAtTop = 0.1f;

    [Header("Boss Health")]
    public int maxHits = 3;
    private int currentHits;

    private bool movingRight = false;
    private bool isActive = false;
    private bool isDefeated = false;

    public GameObject exitObject;
    private float shrinkDuration = 1f;
    private AudioSource audioSource;

    private void Awake()
    {
        currentHits = maxHits;
    }
    private void Start()
    {
        if (exitObject != null)
        {
            exitObject.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void StartBossFight()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(BossSequence());
        }
    }

    private IEnumerator BossSequence()
    {
        while (!isDefeated)
        {
            yield return MoveHorizontallyForRandomDuration();
            yield return PauseAndMoveVertically();
        }
    }

    private IEnumerator MoveHorizontallyForRandomDuration()
    {
        float runDuration = Random.Range(1f, 8f);
        float timer = 0f;

        while (timer < runDuration)
        {
            MoveHorizontally();
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void MoveHorizontally()
    {
        Vector3 position = transform.position;

        if (movingRight)
        {
            position.x += moveSpeed * Time.deltaTime;
            if (position.x >= rightX)
            {
                position.x = rightX;
                ChangeDirection(false);
            }
        }
        else
        {
            position.x -= moveSpeed * Time.deltaTime;
            if (position.x <= leftX)
            {
                position.x = leftX;
                ChangeDirection(true);
            }
        }

        transform.position = position;
    }

    private void ChangeDirection(bool toRight)
    {
        movingRight = toRight;
        transform.localScale = new Vector3(movingRight ? -5f : 5f, 5f, 1f);
    }

    private IEnumerator PauseAndMoveVertically()
    {
        Vector3 startPos = transform.position;
        Vector3 downPos = startPos + Vector3.down * pauseDownDistance;

        yield return MoveToPosition(downPos, verticalMoveSpeed);
        yield return new WaitForSeconds(waitAtBottom);
        yield return MoveToPosition(startPos, verticalMoveSpeed);
        yield return new WaitForSeconds(waitAtTop);
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
    public void TakeHit()
    {
        if (isDefeated) return;

        currentHits--;

        if (currentHits <= 0)
        {
            isDefeated = true;
            isActive = false;
            OnBossDefeated();
        }
    }

    private void OnBossDefeated()
    {
        StartCoroutine(DefeatSequence());

        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.StopCastleMusic();
        }

        if (exitObject != null)
        { 
            exitObject.SetActive(true);

            AudioSource audio = exitObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
        }

    }
    private IEnumerator DefeatSequence()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float timer = 0f;

        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / shrinkDuration);
            yield return null;
        }

        Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}
