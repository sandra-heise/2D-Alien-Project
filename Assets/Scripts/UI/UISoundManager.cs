using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundSelector : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip selectSound;

    private GameObject lastSelected;

    void Start()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    void Update()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;

        if (current != null && current != lastSelected)
        {
            if (selectSound != null && audioSource != null)
                audioSource.PlayOneShot(selectSound);

            lastSelected = current;
        }
    }
}
