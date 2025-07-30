using UnityEngine;

public enum LavaActionType
{
    Start,
    Stop
}

public class LavaTrigger : MonoBehaviour
{
    public LavaActionType actionType;
    public LavaController lavaController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (lavaController == null)
            {
                Debug.LogWarning("LavaController nicht zugewiesen!");
                return;
            }

            if (actionType == LavaActionType.Start)
            {
                lavaController.StartRaising();
            }
            else if (actionType == LavaActionType.Stop)
            {
                lavaController.StopRaising();
            }
        }
    }
}
