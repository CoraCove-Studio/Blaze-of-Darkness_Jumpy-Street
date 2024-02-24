using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        //PlayMusic("Theme"); <-- sample call to play music on load
    }

    public void PlayMusic(string name)
    {
        Sound audio = Array.Find(musicSounds, x => x.name == name);

        if (audio == null)
        {
            Debug.Log("No audio found");
        }

        else
        {
            musicSource.clip = audio.clip;
            musicSource.Play();
        }

    }

    public void PlaySFX(string name)
    {
        Sound audio = Array.Find(sfxSounds, x => x.name == name);

        if (audio == null)
        {
            Debug.Log("No audio found");
        }

        else
        {
            sfxSource.PlayOneShot(audio.clip);
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


// Sample lines to call on collisions or pressed buttons
// AudioManager.Instance.PlaySFX("Jump");
// AudioManager.Instance.PlayMusic("Insert Title name here");
// AudioManager.Instance.musicSource.Stop(); <-- ends music playing will need to be restarted if ended