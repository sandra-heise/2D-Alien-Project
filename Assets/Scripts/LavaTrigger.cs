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
}
