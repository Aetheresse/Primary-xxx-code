﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{




    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {

            if(instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if(instance == null)
                {

                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }

            }

            return instance;

        }
        private set
        {
            instance = value;
        }

    }

    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;

    private void Awake()
    {
        // make sure we don't destroy this instance
        DontDestroyOnLoad(this.gameObject);

        // Create audio source and save them as referancs

        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //Loop the music tracs
        musicSource.loop = true;
        musicSource2.loop = true;


    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;


        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();


    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));

    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;


        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;


        newSource.clip = musicClip;
        
        newSource.Play();



        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));

    }

    private IEnumerator UpdateMusicWithFade (AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();
        
        float t = 0.0f;
        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

       for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume =  (1- t / transitionTime);
           yield return null;
       }

    }


    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
  
 
        float t = 0.0f;
        
        for (t = 0; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }

        original.Stop();
    }



    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        

    }

    public void PlaySFXON(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }


    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;

    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;

    }

}
