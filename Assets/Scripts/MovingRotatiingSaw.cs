using UnityEngine;

public class MovingRotatingSaw : MonoBehaviour
{
    public Transform left;        
    public Transform right;       
    public float moveSpeed = 3f;    
    public float rotationSpeed = 360f; 

    private Vector3 targetPoint;

    void Start()
    {
        targetPoint = right.position;
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, targetPoint) < 0.01f)
        {
            targetPoint = (targetPoint == left.position) ? right.position : left.position;
        }

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
