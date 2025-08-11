using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Image iconImage;           
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    private bool isMuted = false;

    void Start()
    {
        UpdateIcon();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (isMuted)
        {
            iconImage.sprite = soundOffIcon;
        }
        else
        {
            iconImage.sprite = soundOnIcon;
        }
    }
}
