using UnityEngine;

public class IntroScroll : MonoBehaviour
{
    public float scrollSpeed = 50f; 

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
