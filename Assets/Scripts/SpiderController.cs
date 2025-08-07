using System.Collections;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public float leftX = 101f;
    public float rightX = 110f;
    public float moveSpeed = 2f;

    private bool movingRight = false;
    private bool isActive = false;

    public float pauseDownDistance = 2.8f;
    public float verticalMoveSpeed = 3f;
    public float waitAtBottom = 0.5f;

    public void StartBossFight()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(BossSequence());
        }
    }

    private IEnumerator BossSequence()
    {
        while (true)
        {
            float runDuration = Random.Range(1f, 8f);
            float runTimer = 0f;

            while (runTimer < runDuration)
            {
                Vector3 position = transform.position;

                if (movingRight)
                {
                    position.x += moveSpeed * Time.deltaTime;
                    if (position.x >= rightX)
                    {
                        position.x = rightX;
                        movingRight = false;
                        transform.localScale = new Vector3(5f, 5f, 1f);
                    }
                }
                else
                {
                    position.x -= moveSpeed * Time.deltaTime;
                    if (position.x <= leftX)
                    {
                        position.x = leftX;
                        movingRight = true;
                        transform.localScale = new Vector3(-5f, 5f, 1f);
                    }
                }

                transform.position = position;
                runTimer += Time.deltaTime;
                yield return null;
            }


            Vector3 startPos = transform.position;
            Vector3 downPos = startPos + Vector3.down * pauseDownDistance;

            while (Vector3.Distance(transform.position, downPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, downPos, verticalMoveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitAtBottom);

            while (Vector3.Distance(transform.position, startPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, verticalMoveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
