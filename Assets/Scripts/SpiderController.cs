using System.Collections;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public float leftX = 101f;
    public float rightX = 110f;
    public float moveSpeed = 2f;

    private bool movingRight = false;
    private bool isActive = false;

    public void StartBossFight()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(BossSequence()); 
        }
    }

    private IEnumerator BossSequence()
    {       while (true) {
            Vector3 position = transform.position;

            if (movingRight)
            {
                position.x += moveSpeed * Time.deltaTime;
                if (position.x >= rightX)
                {
                    position.x = rightX;
                    movingRight = false;
                    transform.localScale = new Vector3(movingRight ? 5f : 5f, 5f, 1f);
                }
            }
            else
            {
                position.x -= moveSpeed * Time.deltaTime;
                if (position.x <= leftX)
                {
                    position.x = leftX;
                    movingRight = true;
                    transform.localScale = new Vector3(movingRight ? -5f : -5f, 5f, 1f);
                }
            }

            transform.position = position;
            yield return null;
        } 
    }

}
