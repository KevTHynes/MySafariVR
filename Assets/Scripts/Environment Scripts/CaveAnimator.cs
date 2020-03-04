using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CaveAnimator : MonoBehaviour
{
    private Animator anim;

    private AudioSource aud;

    public AudioClip door_Clip;

    void Awake()
    {
        anim = GetComponent<Animator>();

        aud = GetComponentInChildren<AudioSource>();
    }


    public void OpenDoor()
    {
        anim.SetTrigger("Open");

    }
    
    public void PlayDoorAudio()
    {
        aud.clip = door_Clip;
        aud.Play();

    }

    public void StopDoorAudio()
    {
        aud.clip = door_Clip;
        aud.Stop();
    }
}
