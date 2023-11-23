using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Singleton { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private float globalBgmVolume = 1;
    
    public SoundManager()
    {
        Singleton = this;
    }

    private void Awake()
    {
        bgmSource.volume = globalBgmVolume;
    }

    public void StopBackgroundMusic()
    {
        bgmSource.Stop();
    }

    public void PlayBackgroundMusic(AudioClip audioClip, float volume)
    {
        bgmSource.clip = audioClip;
        bgmSource.volume = volume * globalBgmVolume;
        bgmSource.Play();
    }

    public void FadeBackgroundMusicVolume(float from, float to, float seconds, Action callback)
    {
        StartCoroutine(FadeBackgroundMusicVolumeCoroutine(from, to, seconds, callback));
    }


    private IEnumerator FadeBackgroundMusicVolumeCoroutine(float from, float to, float seconds, Action callback)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * (1f/seconds);
            Mathf.Clamp01(t);
            float v = Mathf.Lerp(from, to, t);
            bgmSource.volume = v * globalBgmVolume;
            yield return null;
        }

        if (callback != null)
        {
            callback();
        }
    }

    public void PlaySoundEffect(AudioClip audioClip, float volumeScale)
    {
        effectSource.PlayOneShot(audioClip, volumeScale);   
    }
    
}
