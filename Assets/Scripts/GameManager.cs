using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float finalTime;
    public int collectedCoins;
    public int livesUsed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
