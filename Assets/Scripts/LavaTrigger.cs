using System.Collections;
using UnityEngine;

public enum LavaActionType
{
    Start,
    Stop
}

public class LavaTrigger : MonoBehaviour
{
    public LavaActionType actionType;
    public LavaController lavaController;
    private Animator animator;
    private AudioSource audiossource;
    public Transform platform;
    public Transform targetPoint;   
    public float moveSpeed = 2f;    

    private void Start()
    {
        animator = GetComponent<Animator>();
        audiossource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetBool("isPushed", true);
            }

            if (actionType == LavaActionType.Start)
            {
                lavaController.StartRaising();
            }
            else if (actionType == LavaActionType.Stop)
            {
                lavaController.StopRaising();
                if (platform != null && targetPoint != null)
                {
                    StartCoroutine(MovePlatformToTarget());
                }
            }
        }
        if (audiossource != null)
        {
            audiossource.Play();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (animator != null)
        {
            animator.SetBool("isPushed", false);
        }
    }
    private System.Collections.IEnumerator MovePlatformToTarget()
    {
        while (Vector3.Distance(platform.position, targetPoint.position) > 0.01f)
        {
            platform.position = Vector3.MoveTowards(platform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(FadeAndDestroy());
    }
    private IEnumerator FadeAndDestroy()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        Vector3 originalScale = transform.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}
