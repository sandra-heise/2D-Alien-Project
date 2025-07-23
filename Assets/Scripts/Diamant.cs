using UnityEngine;

public class Diamond : MonoBehaviour
{
    public PowerUpType powerUpType;  // Hier den Typ im Inspector einstellen
    public float powerUpDuration = 45f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerup power = other.GetComponent<PlayerPowerup>();
            if (power != null)
            {
                power.ActivatePowerUp(powerUpType, powerUpDuration);  // �bergib Typ statt Referenz
            }

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), powerUpDuration);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}
