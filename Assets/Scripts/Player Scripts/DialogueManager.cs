using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue[] dialogue;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayDialogue(string name)
    {
        Dialogue d = Array.Find(dialogue, dialogue => dialogue.title == name);
        audioSource.clip = d.clip;
        audioSource.Play();

        //Sound s = Array.Find(sounds, sound => sound.name == name);

        /*
        foreach (Dialogue dia in dialogue)
        {
            name = dia.title;

            audioSource.clip = dia.clip;

            audioSource.Play();     
        }
        */
    }
}
