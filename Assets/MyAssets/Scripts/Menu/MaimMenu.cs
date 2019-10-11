using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaimMenu : MonoBehaviour
{
    public void PlayGame(string sceneToLoad)
	{
		LoadingManager.LoadScene(sceneToLoad);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
