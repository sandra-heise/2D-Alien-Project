using UnityEngine;

public class KeyFollower : MonoBehaviour
{
    private bool isCollected = false;
    private Transform playerTransform;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isCollected && playerTransform != null)
        {
            // Schlüssel folgt dem Spieler leicht versetzt
            Vector3 followPosition = playerTransform.position + new Vector3(0.5f, 0.5f, 0);
            transform.position = Vector3.Lerp(transform.position, followPosition, Time.deltaTime * 5f);
        }
    }

    public void Collect(Transform player)
    {
        isCollected = true;
        playerTransform = player;
    }

    public void ResetKey()
    {
        isCollected = false;
        playerTransform = null;
        transform.position = startPosition;
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
