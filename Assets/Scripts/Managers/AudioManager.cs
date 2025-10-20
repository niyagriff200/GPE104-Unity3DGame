using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    private void Start()
    {
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.spatialBlend = 0f; // 2D sound
        }
    }

    public void PlayMenuMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            musicSource.clip = GameManager.instance.backgroundMenuMusic;
            musicSource.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            musicSource.clip = GameManager.instance.backgroundGameplayMusic;
            musicSource.Play();
        }
    }
}
