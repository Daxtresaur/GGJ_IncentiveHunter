using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioSource Ambience;
    [SerializeField] private AudioSource Music;

    AudioMixer mixer;

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void PlayAmbience(AudioClip clip, bool looping = true)
    {
        Ambience.clip = clip;
        Ambience.loop = looping;
        Ambience.Play();
    }

    public void PlayMusic(AudioClip clip, bool looping = true)
    {
        Ambience.clip = clip;
        Ambience.loop = looping;
        Ambience.Play();
    }
}
