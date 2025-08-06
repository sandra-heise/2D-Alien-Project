using UnityEngine;

public class KeyFollower : MonoBehaviour
{
    private bool isCollected = false;
    private Transform playerTransform;
    private Vector3 startPosition;
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
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
        if (!isCollected)
        {
            isCollected = true;
            playerTransform = player;

            if (audioSource != null)
                audioSource.Play();

            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void ResetKey()
    {
        Debug.Log("ResetKey");
        isCollected = false;
        playerTransform = null;
        transform.position = startPosition;
        GetComponent<Collider2D>().enabled = true;
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
