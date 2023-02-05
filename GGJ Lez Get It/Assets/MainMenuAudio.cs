using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAudio : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip newGameClip;
    [SerializeField] private AudioClip winGameClip;
    [SerializeField] private float duration;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        int hasWon = PlayerPrefs.GetInt("hasWon");
        source.clip = hasWon == 0 ? newGameClip : winGameClip;
        source.Play();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("hasWon", 0);
        StartCoroutine(DoFade(source.volume, 0.0f));
    }

    public IEnumerator DoFade(float start, float end)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            source.volume = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
        //lipat scene
        //SceneManager.LoadScene(sceneName: "PreStartScene");

    }
}
