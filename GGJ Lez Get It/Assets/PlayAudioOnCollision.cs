using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool playOnEnter;
    [SerializeField] private bool playOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!playOnEnter) return;
        SoundManager.instance.PlaySFX(clip);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!playOnExit) return;
        SoundManager.instance.PlaySFX(clip);
    }
}
