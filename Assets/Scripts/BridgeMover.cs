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
    private Vector3 previousPosition;
    public Vector3 PlatformVelocity { get; private set; }


    void Start()
    {
        transform.position = ropeRight.position;
        PlatformVelocity = Vector3.zero;

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
        PlatformVelocity = Vector3.zero;
    }

    IEnumerator MoveTo(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            Vector3 oldPos = transform.position;

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            PlatformVelocity = transform.position - oldPos;

            yield return null;
        }

        transform.position = target;
        PlatformVelocity = Vector3.zero;
    }

}
