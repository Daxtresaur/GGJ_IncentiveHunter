using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private GameObject image;
    VideoPlayer VPlayer;
    // Start is called before the first frame update
    void Start()
    {
        VPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!VPlayer.isPlaying) return;
        if(Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            VPlayer.Stop();
            image.SetActive(false);
        }
    }

    public void PlayMe()
    {
        image.SetActive(true);
        VPlayer.Play();
    }
}
