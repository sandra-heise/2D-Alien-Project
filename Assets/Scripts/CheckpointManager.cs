using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector2 currentCheckpointPosition;
    private static bool checkpointReached = false;
    private AudioSource audioSource;

    [SerializeField] private Sprite activeFlag;
    [SerializeField] private Sprite inactiveFlag;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!checkpointReached)
        {
            SetStartPoint();
        }
    }

    private void Awake()
    {
        if (!checkpointReached)
        {
            SetStartPoint();
         }
    }
    private void SetStartPoint()
    {
        GameObject startObj = GameObject.Find("StartPoint");
        if (startObj != null)
        {
            currentCheckpointPosition = startObj.transform.position;
        }
    }
    public void ActivateCheckpoint(Transform checkpointTransform)
    {
        currentCheckpointPosition = checkpointTransform.position;
        checkpointReached = true;
        if (audioSource != null)
            audioSource.Play();

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
        GameObject startObj = GameObject.Find("StartPoint");
        if (startObj != null)
        {
            currentCheckpointPosition = startObj.transform.position;
        }
        foreach (GameObject flag in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            SpriteRenderer sr = flag.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                CheckpointManager manager = flag.GetComponent<CheckpointManager>();
                if (manager != null)
                {
                    sr.sprite = manager.inactiveFlag;
                }
            }
        }
    }
}
