using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float moveDistance = 1f;    
    public float moveSpeed =1f;        
    private Vector2 startPosition;
    private bool movingRight = false;
    private SpriteRenderer sr;

    void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveDirection = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * moveDirection);

        
        if (sr != null)
            sr.flipX = movingRight;

        
        if (movingRight && transform.position.x >= startPosition.x + moveDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= startPosition.x - moveDistance)
        {
            movingRight = true;
        }
    }
}
