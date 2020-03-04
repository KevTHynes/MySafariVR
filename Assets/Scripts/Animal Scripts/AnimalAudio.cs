using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip roar_Clip, attack_Clip, happy_Clip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Play_RoarSound()
    {
        audioSource.clip = roar_Clip;
        audioSource.Play();
    }
    public void Play_AttackSound()
    {
        audioSource.clip = attack_Clip;
        audioSource.Play();
    }
    public void Play_HappySound()
    {
        audioSource.clip = happy_Clip;
        audioSource.Play();
    }

}
