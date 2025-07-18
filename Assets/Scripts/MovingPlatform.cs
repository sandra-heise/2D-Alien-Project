using UnityEngine;

public class PlatformBetweenPoints : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 0.5f;

    private Vector3 target;
    private float offset;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Fehlende Punkte für Plattformbewegung!");
            enabled = false;
            return;
        }

        offset = Random.Range(0f, 1f);
        // Startposition bestimmen (z. B. näher an pointA oder pointB)
        float startLerp = Mathf.PingPong(Time.time * moveSpeed + offset, 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, startLerp);
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed + offset, 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}
