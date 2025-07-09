using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public BridgeMover bridgeMover;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bridgeMover != null)
            {
                bridgeMover.ActivateBridge();
            }         
        }
    }

}
