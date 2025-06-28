using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioSetting[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()//chay xuyen suot
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

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        AudioSetting s = Array.Find(musicSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        AudioSetting s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopMusic(string name)
    {
        AudioSetting s = Array.Find(musicSound, x => x.name == name);
        if (s != null && musicSource.clip == s.clip && musicSource.isPlaying)
        {
            musicSource.Stop(); // Dừng nhạc nếu trùng khớp tên
        }
    }


    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop(); // Dừng phát hiệu ứng âm thanh
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume) 
    {
        sfxSource.volume = volume;   
    }
}
