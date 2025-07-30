using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveInterval = 3f;
    private Vector3 startPosition;

    private Coroutine raisingCoroutine;
    void Start()
    {
        startPosition = transform.position;
    }
    public void StartRaising()
    {
        if (raisingCoroutine == null)
        {
            raisingCoroutine = StartCoroutine(RaiseLava());
        }
    }

    public void StopRaising()
    {
        if (raisingCoroutine != null)
        {
            StopCoroutine(raisingCoroutine);
            raisingCoroutine = null;
        }
    }

    private IEnumerator RaiseLava()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);
            transform.position += new Vector3(0f, moveDistance, 0f);
        }
    }
    public void ResetLava()
    {
        transform.position = startPosition;
        StopAllCoroutines();
        raisingCoroutine = null;
    }

}
