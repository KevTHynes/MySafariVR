using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnclosureAudio : MonoBehaviour
{
    public Sound[] sounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        
    }

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            /*
            //s.source = gameObject.AddComponent<AudioSource>();
            s.source = GetComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.priority = s.priority;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.spatialBlend = s.spatial_Blend;
            s.source.loop = s.loop;
            */

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.priority = s.priority;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.spatialBlend = s.spatial_Blend;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
            s.source.loop = s.loop;

        }
        //PlaySound("Waves");

    }

    public void GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source = audioSource;
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }
}
