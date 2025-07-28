using UnityEngine;

public class Diamond : MonoBehaviour
{
    public PowerUpType powerUpType;  
    public float powerUpDuration = 45f;
    public static Diamond lastCollectedDiamond;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerup power = other.GetComponent<PlayerPowerup>();
            if (power != null)
            {
                power.ActivatePowerUp(powerUpType, powerUpDuration); 
            }

            lastCollectedDiamond = this;
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), powerUpDuration);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}
