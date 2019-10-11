#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	[Header("Loading Scene Visuals")]

	[SerializeField]
	[Tooltip("Overlay Image which hides the scenes in background")]
	private Image _sceneFadeOverlay;

	[SerializeField]
	[Tooltip("Slider which serves as progress bar for loading")]
	private Slider _progressSlider;

	[SerializeField]
	[Tooltip("Overlay Image of the Loading scene")]
	private Image _loadingSceneOverlay;

	[Header("Loading Scene settings")]

	[SerializeField]
	[Tooltip("How long in seconds does the fade effect take")]
	private float _fadeDuration = 0.5f;

	[Header("Loading Scene")]

	public static string LoadingSceneName = "SCN_LoadingScene";

	[HideInInspector]
	public static string SceneToLoad = "";

	private AsyncOperation _loadingAsyncOperation;

	private void Awake()
	{
		_sceneFadeOverlay.gameObject.SetActive(true);
		_progressSlider.gameObject.SetActive(false);
		_loadingSceneOverlay.gameObject.SetActive(false);

		StartCoroutine(LoadSceneAsync(SceneToLoad));
	}

	public static void LoadScene(string sceneToLoad)
	{
		SceneToLoad = sceneToLoad;
		SceneManager.LoadScene(LoadingSceneName, LoadSceneMode.Additive);
	}

	private IEnumerator Fade(Image image, float fadeDuration, bool isFadingIn)
	{
		float startAlpha = isFadingIn ? 0.0f : 1.0f;
		float endAlpha = isFadingIn ? 1.0f : 0.0f;

		Color color = image.color;
		color.a = startAlpha;

		float stopWatch = 0.0f;
		while(stopWatch < fadeDuration)
		{
			stopWatch += Time.deltaTime;
			color.a = Mathf.Lerp(startAlpha, endAlpha, stopWatch / fadeDuration);
			image.color = color;
			yield return null;
		}
		color.a = endAlpha;
		image.color = color;
	}

	private IEnumerator LoadSceneAsync(string sceneName)
	{
		yield return StartCoroutine(Fade(_loadingSceneOverlay, _fadeDuration, true));
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		_progressSlider.gameObject.SetActive(true);
		_loadingSceneOverlay.gameObject.SetActive(true);

		yield return StartCoroutine(Fade(_loadingSceneOverlay, _fadeDuration, false));

		_loadingAsyncOperation = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
		_loadingAsyncOperation.allowSceneActivation = false;

		float loadingProgreess = 0.0f;
		while (_loadingAsyncOperation.progress < 0.9f)
		{
			yield return null;
			if(!Mathf.Approximately(_loadingAsyncOperation.progress, loadingProgreess))
			{
				loadingProgreess = _loadingAsyncOperation.progress;
				_progressSlider.value = loadingProgreess;
			}
		}

		_progressSlider.value = 1.0f;

		_loadingAsyncOperation.allowSceneActivation = true;

		_progressSlider.gameObject.SetActive(false);
		_loadingSceneOverlay.gameObject.SetActive(false);

		yield return StartCoroutine(Fade(_sceneFadeOverlay, _fadeDuration, false));

		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(LoadingSceneName));
	}

}
