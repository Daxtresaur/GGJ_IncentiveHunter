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
        VPlayer.loopPointReached += loopPointReached;
    }

    private void OnDisable()
    {
        VPlayer.loopPointReached -= loopPointReached;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"{VPlayer.frame} {VPlayer.frameCount}");

        if (!VPlayer.isPlaying) return;
        if(Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            StopMe();
        }
    }

    public void PlayMe()
    {
        image.SetActive(true);
        VPlayer.Play();
    }

    public void StopMe()
    {
        VPlayer.Stop();
        image.SetActive(false);
    }

    public virtual void loopPointReached(VideoPlayer vp)
    {
        StopMe();
    }
}
