using UnityEngine;

public class GameUIManager : MonoBehaviour
{
	[SerializeField] private GameObject gameplayUI;
	[SerializeField] private GameObject gameplayPausedUI;
	[SerializeField] private GameObject gameOverUI;
	[SerializeField] private GameObject victoryUI;

	private GameplayState state = GameplayState.None;

	public GameplayState State
	{
		get
		{
			return state;
		}
		set
		{
			state = value;
			switch (value)
			{
				case GameplayState.Playing:
					DisableAllUIs();
					if (gameplayUI)
						gameplayUI.SetActive(true);
					break;
				case GameplayState.Paused:
					DisableAllUIs();
					if (gameplayPausedUI)
						gameplayPausedUI.SetActive(true);
					break;
				case GameplayState.GameOver:
					DisableAllUIs();
					if (gameOverUI)
						gameOverUI.SetActive(true);
					break;
				case GameplayState.Victory:
					DisableAllUIs();
					if (victoryUI)
						victoryUI.SetActive(true);
					break;
				default:
				case GameplayState.None:
					DisableAllUIs();
					break;
			}
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
		if (victoryUI)
			victoryUI.SetActive(false);
	}
}
