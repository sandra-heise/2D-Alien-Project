using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorSprite;      // Geschlossene T�r
    public GameObject lockObject;      // Schloss
    public Sprite openDoorSprite;      // Sprite der offenen T�r
    public GameObject keyFollow;       // Der Schl�ssel, der folgen kann

    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger betreten von: " + collision.name);
        if (collision.CompareTag("Player") && keyFollow != null )
        {

            KeyFollower keyFollowerScript = keyFollow.GetComponent<KeyFollower>();

            if (keyFollowerScript != null && keyFollowerScript.IsCollected())
            {
                Debug.Log("Key wird deaktiviert: " + keyFollow.name);
                keyFollow.SetActive(false);
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;

        // T�r-Sprite �ndern
        doorSprite.GetComponent<SpriteRenderer>().sprite = openDoorSprite;

        
        // Schloss und Schl�ssel deaktivieren
        if (lockObject != null) lockObject.SetActive(false);
        if (keyFollow != null) keyFollow.SetActive(false);


        // collider entfernen
        Collider2D doorCollider = doorSprite.GetComponent<Collider2D>();
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}
