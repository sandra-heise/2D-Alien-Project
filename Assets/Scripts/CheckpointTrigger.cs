using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private CheckpointManager manager;
    private bool isActivated = false;

    private void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            manager.ActivateCheckpoint(transform);
        }
    }
}
