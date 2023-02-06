using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HorrorAmbiance : MonoBehaviour
{
    public static HorrorAmbiance Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float minRandomRange;
    [SerializeField] private float maxRandomRange;

    private bool isPlaying;
    private bool canPlay;
    public bool CanPlay
    {
        get { return canPlay; }
        set 
        { 
            canPlay = value;
            if (canPlay) return;
            SoundManager.instance.StopAmbience();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        SoundManager.instance.PlayMusic(clips[3], true);
        StartCoroutine(LoopHorror());
    }

    private void OnDisable()
    {
        isPlaying = false;
        StopAllCoroutines();
    }

    IEnumerator LoopHorror()
    {
        while (isPlaying)
        {
            Debug.Log("Playing Ambiance");
            float range = Random.Range(minRandomRange, maxRandomRange);
            int index= Random.Range(0, clips.Length);
            yield return new WaitForSeconds(range);
            if (canPlay)
            {
                SoundManager.instance.PlayAmbience(clips[index], false);
            }
        }
    }
}
