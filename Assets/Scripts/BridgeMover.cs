// Dieses Script kommt an das Bridge-Objekt (nicht an das Kind!)

using UnityEngine;
using System.Collections;

public class BridgeMover : MonoBehaviour
{
    public Transform ropeLeft;
    public Transform rope;
    public Transform ropeRight;
    public float speed = 2f;
    public float pauseTime = 1f;

    private bool isMoving = false;

    void Start()
    {
        transform.position = ropeRight.position;
    }

    public void ActivateBridge()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveBridge());
        }
    }

    IEnumerator MoveBridge()
    {
        isMoving = true;

        yield return MoveTo(rope.position);
        yield return MoveTo(ropeLeft.position);

        yield return new WaitForSeconds(pauseTime);

        yield return MoveTo(ropeRight.position);

        isMoving = false;
    }

    IEnumerator MoveTo(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }
}
