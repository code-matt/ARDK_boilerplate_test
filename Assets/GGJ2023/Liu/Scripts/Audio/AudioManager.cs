using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backgroundMusic;
    public AudioSource soundEffectSource;

    public AudioClip[] audioClips;
    public Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        backgroundMusic.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip)
        {
            soundEffectSource.clip = clip;
            soundEffectSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + clip);
        }
    }

    public void StopSoundEffect()
    {
        soundEffectSource.Stop();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
}
