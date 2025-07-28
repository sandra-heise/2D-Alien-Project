using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopUIManager shopUIManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUIManager.OpenShop();
        }
    }
}
