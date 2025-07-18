using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private CheckpointManager manager;

    private void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.ActivateCheckpoint(transform);
        }
    }
}
