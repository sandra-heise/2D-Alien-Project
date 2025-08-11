using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;

    private AudioSource audioSource;
    public AudioSource powerUpMusic;
    public AudioSource castleMusic;
    public float fadeDuration = 1.5f;
    void Awake()
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

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip musicClip, float volume = 0.3f)
    {
        if (audioSource.clip == musicClip) return; // Nicht erneut abspielen

        audioSource.Stop();
        audioSource.clip = musicClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void PlayPowerUpMusic()
    {
        StartCoroutine(Crossfade(audioSource, powerUpMusic));
    }
    public void PlayCastleMusic()
    {
        StartCoroutine(Crossfade(audioSource, castleMusic));
    }

    public void StopPowerUpMusic()
    {
        StartCoroutine(Crossfade(powerUpMusic, audioSource));
    }

    public void StopCastleMusic()
    {
        StartCoroutine(Crossfade(castleMusic, audioSource));
    }
    private IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        float time = 0f;

        to.volume = 0f;
        if (!to.isPlaying)
            to.Play();

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            from.volume = Mathf.Lerp(1f, 0f, t);
            to.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        from.volume = 0f;
        from.Pause();

        to.volume = 1f;
    }
}
