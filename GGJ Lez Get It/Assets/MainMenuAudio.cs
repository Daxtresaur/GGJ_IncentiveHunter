using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip newGameClip;
    [SerializeField] private AudioClip winGameClip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        int hasWon = PlayerPrefs.GetInt("hasWon");
        source.clip = hasWon == 0 ? newGameClip : winGameClip;
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("hasWon", 0);
    }
}
