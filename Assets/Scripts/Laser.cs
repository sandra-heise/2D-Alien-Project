using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private Vector2 direction = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") || collision.CompareTag("snakeLava"))
        {
            if (collision.CompareTag("snakeLava"))
            {
                Destroy(collision.gameObject); // Gegner verschwinden lassen
            }

            Destroy(gameObject); // Laser verschwindet
        }
    }
}
