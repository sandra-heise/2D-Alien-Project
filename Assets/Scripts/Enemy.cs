using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private AudioSource audioSource;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private PlayerMovement FindPlayerMovement(GameObject obj)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            PlayerMovement pm = current.GetComponent<PlayerMovement>();
            if (pm != null)
                return pm;
            current = current.parent;
        }
        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 enemyCenter = GetComponent<Collider2D>().bounds.center;

            bool hitFromAbove = contactPoint.y > enemyCenter.y + 0.1f;

            Debug.Log(contactPoint.y + " " + enemyCenter.y)  ;

            if (hitFromAbove)
            {
                PlayerMovement playerMovement = FindPlayerMovement(collision.gameObject);
                if (playerMovement != null)
                {
                    playerMovement.Bounce();
                }
               
                Die();
            }
            else
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.StartCoroutine(playerHealth.LoseLife());
                }
            }
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (audioSource != null)
            audioSource.Play();

        animator.SetTrigger("isDead");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
}
