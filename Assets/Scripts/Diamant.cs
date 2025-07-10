using UnityEngine;

public class Diamond : MonoBehaviour
{
    public float respawnTime = 60f; // wie lange der Diamant wegbleibt

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerup power = other.GetComponent<PlayerPowerup>();
            if (power != null)
            {
                power.ActivatePowerUp(this); // übergeben der Referenz
            }

            gameObject.SetActive(false); // statt Destroy
            Invoke(nameof(Respawn), respawnTime);     // respawn verzögert aufrufen
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}
