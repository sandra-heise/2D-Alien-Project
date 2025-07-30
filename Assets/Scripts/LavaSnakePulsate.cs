using UnityEngine;

public class LavaSnakePulsate : MonoBehaviour
{
    public float minScaleY = 1f;      
    public float maxScaleY = 2f;       
    public float speed = 2f;          

    private float originalScaleX;
    private float originalScaleZ;

    void Start()
    {
        originalScaleX = transform.localScale.x;
        originalScaleZ = transform.localScale.z;
    }

    void Update()
    {
        float scaleY = Mathf.Lerp(minScaleY, maxScaleY, (Mathf.Sin(Time.time * speed) + 1f) / 2f);
        transform.localScale = new Vector3(originalScaleX, scaleY, originalScaleZ);
    }
}
