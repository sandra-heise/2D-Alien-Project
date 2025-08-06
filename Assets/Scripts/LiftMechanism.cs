using UnityEngine;
using System.Collections;

public class LiftMechanism : MonoBehaviour
{
    public Transform liftLeft;
    public Transform liftRight;
    public Transform liftPlattform;
    public Transform liftTopPoint;

    private SpriteRenderer spriteRenderer;
    public Sprite liftdefaultSprite;
    public Sprite liftactiveSprite;

    public float liftSpeed = 2f;
    private bool playerInRange = false;
    private bool isSwitched = false;
    private bool isMoving = false;

    private Vector3 liftStartPos;
    private Vector3 liftEndPos;
    private AudioSource switchSound;

    private void Start()
    {
        liftStartPos = liftPlattform.position;
        liftEndPos = liftTopPoint.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = liftdefaultSprite;
        switchSound = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            if (switchSound != null)
                switchSound.Play();
            ToggleLever();
        }
    }

    private void ToggleLever()
    {
        if (isMoving) return;
        isSwitched = true;
        spriteRenderer.sprite = liftactiveSprite;
        StartCoroutine(MoveLift(true)); // nur nach oben bewegen
    }

    private IEnumerator MoveLift(bool goingUp)
    {
        isMoving = true;

        Vector3 targetPos = goingUp ? liftEndPos : liftStartPos;
        Vector3 startPos = liftPlattform.position;

        float totalDistance = Vector3.Distance(startPos, targetPos);
        float duration = totalDistance / liftSpeed;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            liftPlattform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        liftPlattform.position = targetPos;

        if (goingUp)
        {
            yield return new WaitForSeconds(2.5f); 
            StartCoroutine(MoveLift(false));
        }
        else
        {
            isSwitched = false;
            spriteRenderer.sprite = liftdefaultSprite;
            isMoving = false;
        }

        // Wenn Rückfahrt war, wird oben nicht mehr gestoppt
        if (!goingUp)
        {
            isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
