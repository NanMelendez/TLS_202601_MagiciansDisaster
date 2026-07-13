using System;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
	[SerializeField] private GameObject gameplayUI;
	[SerializeField] private GameObject gameplayPausedUI;
	[SerializeField] private GameObject gameOverUI;
	[SerializeField] private TextMeshProUGUI timerText;

	private GameplayState state = GameplayState.None;

	public GameplayState State
	{
		get
		{
			return state;
		}
		set
		{
			// Debug.Log($"Cambiando a: {value}");
			state = value;
			switch (value)
			{
				case GameplayState.Playing:
					DisableAllUIs();
					gameplayUI.SetActive(true);
					break;
				case GameplayState.Paused:
					DisableAllUIs();
					gameplayPausedUI.SetActive(true);
					break;
				case GameplayState.GameOver:
					DisableAllUIs();
					gameOverUI.SetActive(true);
					break;
				default:
				case GameplayState.None:
					DisableAllUIs();
					break;
			}
		}
	}

	public float ElapsedTime
	{
		set
		{
			timerText.text = TimeSpan.FromSeconds(value).ToString("mm\\:ss");
		}
	}

	private void DisableAllUIs()
	{
		if (gameplayUI)
			gameplayUI.SetActive(false);
		if (gameplayPausedUI)
			gameplayPausedUI.SetActive(false);
		if (gameOverUI)
			gameOverUI.SetActive(false);
	}
}
