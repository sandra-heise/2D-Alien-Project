using UnityEngine;

public class IntroPlayerController : MonoBehaviour
{
    public Transform targetPoint;     
    private float moveSpeed = 180f;

    private void Update()
    {
      
      transform.position += Vector3.right * moveSpeed * Time.deltaTime;
      
    }
}
