using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioSource Ambience;
    [SerializeField] private AudioSource Music;

    AudioMixer mixer;

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
