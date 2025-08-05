using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 0.5f;

    private Vector3 target;
    private float offset;
    private Vector3 previousPosition;
    public Vector3 PlatformVelocity { get; private set; }


    void Start()
    {
        previousPosition = transform.position;
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Fehlende Punkte für Plattformbewegung!");
            enabled = false;
            return;
        }

        offset = Random.Range(0f, 1f);
        
        float startLerp = Mathf.PingPong(Time.time * moveSpeed + offset, 1f);
        Vector3 newPosition = Vector3.Lerp(pointA.position, pointB.position, startLerp);
        newPosition.z = transform.position.z; // ursprünglichen Z-Wert behalten
        transform.position = newPosition;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed + offset, 1f);
        Vector3 newPosition = Vector3.Lerp(pointA.position, pointB.position, t);
        newPosition.z = transform.position.z; // Z-Wert nicht überschreiben
        transform.position = newPosition;
    }
    void LateUpdate()
    {
        PlatformVelocity = transform.position - previousPosition;
        previousPosition = transform.position;
    }
}
