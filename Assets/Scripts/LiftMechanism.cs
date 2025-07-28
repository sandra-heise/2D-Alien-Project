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

    private void Start()
    {
        liftStartPos = liftPlattform.position;
        liftEndPos = liftTopPoint.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = liftdefaultSprite;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            ToggleLever();
        }
    }

    private void ToggleLever()
    {
        isSwitched = !isSwitched;
        spriteRenderer.sprite = isSwitched ? liftactiveSprite : liftdefaultSprite;

        StartCoroutine(MoveLift(isSwitched));
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
        isMoving = false;
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
