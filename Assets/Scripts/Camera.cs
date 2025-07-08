using UnityEngine;

public class FreeCameraMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float fixedZ; // Z-Wert zwischenspeichern

    void Start()
    {
        fixedZ = transform.position.z; // Start-Z merken
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0f) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Z-Wert fixieren
        transform.position = new Vector3(transform.position.x, transform.position.y, fixedZ);
    }
}
