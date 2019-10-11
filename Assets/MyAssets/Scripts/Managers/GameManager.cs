#pragma warning disable 649

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[Header("UI:")]

	[SerializeField]
	[Tooltip("GameObject containing canvas which holds gameplay UI")]
	private GameObject _gameplayUIGameObject;

	[SerializeField]
	[Tooltip("GameObject containing pause manu canvas")]
	private GameObject _pauseMenuGameObject;

	[SerializeField]
	[Tooltip("GameObject containing the end-game canvas")]
	private GameObject _endGameUIGameObject;

	[SerializeField]
	[Tooltip("Count text for green cubes")]
	private Text _greenCubeCountText;

	[SerializeField]
	[Tooltip("Count text for yellow cubes")]
	private Text _yellowCubeCountText;

	[SerializeField]
	[Tooltip("Text box for end game defeat/victory text")]
	private Text _endGameText;

	[SerializeField]
	[Tooltip("Text displayed at the end of a game if player is victorious")]
	private string _victoriousText = "Congratulations, you defeated all the enemies and collected neeeded cubes";

	[SerializeField]
	[Tooltip("Text displayed at the end of a game if player is defeated")]
	private string _defeatedText = "You were defeeated, better luck next time";


	private int _totalYellowCubeCount;
	private int _totalGreenCubeCount;
	private int _currentYellowCubeCount = 0;
	private int _currentGreeenCubeCount = 0;
	private List<GameObject> _enemiesGameObjects = new List<GameObject>();

	//singleton
	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			return instance;
		}
	}

	private void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

		GameObject[] greenCubes = GameObject.FindGameObjectsWithTag("GreenCube");
		GameObject[] yellowCubes = GameObject.FindGameObjectsWithTag("YellowCube");

		_totalGreenCubeCount = greenCubes.GetLength(0);
		_totalYellowCubeCount = yellowCubes.GetLength(0);

		_enemiesGameObjects = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();

		_greenCubeCountText.text = _currentGreeenCubeCount.ToString() + " / " + _totalGreenCubeCount.ToString();
		_yellowCubeCountText.text = _currentYellowCubeCount.ToString() + " / " + _totalYellowCubeCount.ToString();

		LockCursor();
		_gameplayUIGameObject.SetActive(true);
		_pauseMenuGameObject.SetActive(false);
		_endGameUIGameObject.SetActive(false);
	}

	private void Update()
	{
		if(Input.GetButtonDown("Cancel"))
		{
			ToggleMenu();
		}
	}

	public void CollectGreenCube()
	{
		_currentGreeenCubeCount += 1;
		_greenCubeCountText.text = _currentGreeenCubeCount.ToString() + " / " + _totalGreenCubeCount.ToString();
	}

	public void CollectYellowCube()
	{
		_currentYellowCubeCount += 1;
		_yellowCubeCountText.text = _currentYellowCubeCount.ToString() + " / " + _totalYellowCubeCount.ToString();
	}

	public void RestartLevel()
	{
		Time.timeScale = 1.0f;
		LoadingManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ExitGame()
	{
		Time.timeScale = 1.0f;
		Application.Quit();
	}

	public void TriggerEndGame(bool isVictorious)
	{
		UnlockCursor();
		_gameplayUIGameObject.SetActive(false);
		_endGameUIGameObject.SetActive(true);

		if(isVictorious)
		{
			_endGameText.text = _victoriousText;
		}
		else
		{
			_endGameText.text = _defeatedText;
		}
	}

	private void ToggleMenu()
	{
		if(_gameplayUIGameObject.activeInHierarchy)
		{
			UnlockCursor();
			_gameplayUIGameObject.SetActive(false);
			_pauseMenuGameObject.SetActive(true);
			Time.timeScale = 0.0f;
		}
		else
		{
			LockCursor();
			_gameplayUIGameObject.SetActive(true);
			_pauseMenuGameObject.SetActive(false);
			Time.timeScale = 1.0f;
		}
	}

	private void LockCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void UnlockCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void CheckIfGameIsWon()
	{
		if(_currentGreeenCubeCount == _totalGreenCubeCount && _currentYellowCubeCount == _totalYellowCubeCount)
		{
			bool enemiesDead = true;
			foreach(GameObject enemyGameObject in _enemiesGameObjects)
			{
				if(enemyGameObject.activeInHierarchy)
				{
					enemiesDead = false;
					break;
				}
			}
			if(enemiesDead)
			{
				TriggerEndGame(true);
			}
		}
	}

}
