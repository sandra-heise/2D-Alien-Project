using UnityEngine;
using UnityEngine.Audio;

public class LeverMechanism : MonoBehaviour
{
    public Transform chainLeft;
    public Transform chainRight;

    public Vector3 chainLeftUpPosition;
    public Vector3 chainLeftDownPosition;
    public Vector3 chainRightUpPosition;
    public Vector3 chainRightDownPosition;

    public Sprite leverLeftSprite;
    public Sprite leverRightSprite;

    private SpriteRenderer spriteRenderer;
    private bool isSwitched = false;
    private bool playerInRange = false;
    private AudioSource switchSound;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leverRightSprite; // Startzustand: linke Kette unten
        SetChainPositions();
        switchSound = GetComponent<AudioSource>();
    
}

    private void Update()
    {
        // Spieler ist in Reichweite und drückt Taste (z. B. E)
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (switchSound != null)
                switchSound.Play();
            ToggleLever();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void ToggleLever()
    {
        isSwitched = !isSwitched;

        spriteRenderer.sprite = isSwitched ? leverLeftSprite : leverRightSprite;

        SetChainPositions();
    }

    private void SetChainPositions()
    {
        if (isSwitched)
        {
            chainLeft.position = chainLeftUpPosition;
            chainRight.position = chainRightDownPosition;
        }
        else
        {
            chainLeft.position = chainLeftDownPosition;
            chainRight.position = chainRightUpPosition;
        }
    }
    public void ResetMechanism()
    {
        isSwitched = false;
        spriteRenderer.sprite = leverRightSprite;
        SetChainPositions();
    }
}
