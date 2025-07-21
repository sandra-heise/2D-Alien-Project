using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector2 currentCheckpointPosition;
    private static bool checkpointReached = false;

    [SerializeField] private Sprite activeFlag;
    [SerializeField] private Sprite inactiveFlag;

    private void Awake()
    {
        // Beim Neustart des Spiels zurück zum Startpunkt
        if (!checkpointReached)
        {
            currentCheckpointPosition = GameObject.Find("StartPoint").transform.position;
        }
    }

    public void ActivateCheckpoint(Transform checkpointTransform)
    {
        currentCheckpointPosition = checkpointTransform.position;
        checkpointReached = true;

        foreach (GameObject flag in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            SpriteRenderer sr = flag.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = (flag.transform == checkpointTransform) ? activeFlag : inactiveFlag;
            }
        }
    }

    public static void ResetCheckpoints()
    {
        checkpointReached = false;
    }
}
