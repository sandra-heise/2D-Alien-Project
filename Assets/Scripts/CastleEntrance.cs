using Unity.VisualScripting;
using UnityEngine;

public class CastleEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.PlayCastleMusic();
        }
    }
}
