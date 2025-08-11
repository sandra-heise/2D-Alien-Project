using UnityEngine;

public class IntroTextController : MonoBehaviour
{
    public GameObject introText;

    void Start()
    {
        introText.SetActive(true);
        Invoke(nameof(HideText), 5f);
    }

    void HideText()
    {
        introText.SetActive(false);
    }
}
