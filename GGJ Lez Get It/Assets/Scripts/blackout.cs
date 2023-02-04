using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class blackout : MonoBehaviour
{

	public CanvasGroup canvGroup;
	public Image image;
	public float duration = 5f;
	private bool mFaded = false;
	public AudioSource treeChop;
    public GameObject canvas;

	private void Start()
	{
		//StartCoroutine(DoFadeWin(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));

	}
	public void Fade()
	{

		StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));

		mFaded = !mFaded;
	}


	public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
	{
		float counter = 0f;
		while (counter < duration)
		{

			counter += Time.deltaTime;
			canvGroup.alpha = Mathf.Lerp(start, 1, counter / duration);

			yield return null;
		}
		//lipat scene
		SceneManager.LoadScene(sceneName: "PreStartScene");

	}

	public void FadeWin()
	{

		StartCoroutine(DoFadeWin(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));

		mFaded = !mFaded;
	}

	public IEnumerator DoFadeWin(CanvasGroup canvGroup, float start, float end)
	{
		float counter = 0f;
		while (counter < duration)//fade to black
		{

			counter += Time.deltaTime;
			canvGroup.alpha = Mathf.Lerp(start, 1, counter / duration);

			yield return null;
		}
		
		treeChop.Play();
		yield return new WaitWhile(() => treeChop.isPlaying);


		counter = 0f;
		var tempColor = image.color;

		while (counter < duration)//fade to white
		{

			counter += Time.deltaTime;
			tempColor = Color.Lerp(Color.black, Color.white, counter / duration);
			image.color = tempColor;

			yield return null;
		}
		this.gameObject.SetActive(false);
		this.canvas.SetActive(true);

	}

    public void ExitGame()
    {
		Application.Quit();
		Debug.Log("Quit Game");
    }

}
