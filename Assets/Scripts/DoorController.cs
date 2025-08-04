using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorSprite;      // Geschlossene Tür
    public GameObject lockObject;      // Schloss
    public Sprite openDoorSprite;      // Sprite der offenen Tür
    public GameObject keyFollow;       // Der Schlüssel, der folgen kann
    private AudioSource audioSource;

    private bool isOpen = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && keyFollow != null )
        {

            KeyFollower keyFollowerScript = keyFollow.GetComponent<KeyFollower>();

            if (keyFollowerScript != null && keyFollowerScript.IsCollected())
            {
                keyFollow.SetActive(false);
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        if (audioSource != null)
            audioSource.Play();

        doorSprite.GetComponent<SpriteRenderer>().sprite = openDoorSprite;

        if (lockObject != null) lockObject.SetActive(false);
        if (keyFollow != null) keyFollow.SetActive(false);


        Collider2D doorCollider = doorSprite.GetComponent<Collider2D>();
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}
