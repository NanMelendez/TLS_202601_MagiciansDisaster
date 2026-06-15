using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private CinemachineCamera cmCam;
	[SerializeField] private GameUIManager UIManager;
	[SerializeField] private InputActionReference pauseAction;

	private GameplayState state;
	private PlayerController pController;

	private void Awake()
	{
		pController = player.GetComponent<PlayerController>();
		state = GameplayState.Playing;

		Time.timeScale = 1f;
		UIManager.State = state;
		pController.state = state;
	}

	private void OnEnable()
	{
		pauseAction.action.Enable();
	}

	private void OnDisable()
	{
		pauseAction.action.Disable();
	}

	private void Update()
	{
		if (player)
		{
			Health playerHealth = player.GetComponent<Health>();
			if (!playerHealth.IsAlive)
			{
				cmCam.Follow = null;
				state = GameplayState.GameOver;
				UIManager.State = state;
				pController.state = state;
			}
		}

		if (pauseAction)
			if (pauseAction.action.IsPressed())
				PauseGame();
	}

	private void PauseGame()
	{
		state = GameplayState.Paused;
		UIManager.State = state;
		pController.state = state;
		Time.timeScale = 0.0f;
	}

	public void ContinueGame()
	{
		state = GameplayState.Playing;
		UIManager.State = state;
		pController.state = state;
		Time.timeScale = 1.0f;
	}
}
