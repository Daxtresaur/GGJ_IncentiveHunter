using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class blackout : MonoBehaviour
{

	public CanvasGroup canvGroup;
	public float duration = 5f;
	private bool mFaded = false;

	private void Start()
	{
		
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

}
