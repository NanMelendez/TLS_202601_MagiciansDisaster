using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private CinemachineCamera cmCam;
	[SerializeField] private GameUIManager UIManager;
	[SerializeField] private InputActionReference pauseAction;
	[SerializeField] private TextMeshProUGUI elapsedTimeGameOver;
	[SerializeField] private GameObject manualPanel;

	private GameplayState state;
	private PlayerController pController;

	private float elapsedTime;

	private bool activeTimer = true;

	public float ElapsedTime
	{
		get { return elapsedTime; }
	}

	private void Awake()
	{
		pController = player.GetComponent<PlayerController>();
		state = GameplayState.Playing;

		Time.timeScale = 1f;
		UIManager.State = state;
		pController.state = state;

		elapsedTime = 0.0f;

		manualPanel.SetActive(false);
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
		UIManager.ElapsedTime = elapsedTime;

		if (player)
		{
			if (activeTimer)
				elapsedTime += Time.deltaTime;

			Health playerHealth = player.GetComponent<Health>();
			if (!playerHealth.IsAlive)
			{
				cmCam.Follow = null;
				state = GameplayState.GameOver;
				pController.state = state;
				StartCoroutine(ChangeUIStartAfterGameOver());
				elapsedTimeGameOver.text = "Sobreviviste: " + TimeSpan.FromSeconds(elapsedTime).ToString("mm\\:ss");
			}
		}

		if (pauseAction)
			if (pauseAction.action.IsPressed())
			{
				PauseGame();
				activeTimer = false;
			}
	}

	private IEnumerator ChangeUIStartAfterGameOver()
	{
		yield return new WaitForSeconds(pController.destroyAfterSeconds);
		UIManager.State = state;
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
		activeTimer = true;
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GoToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void ShowManual()
	{
		manualPanel.SetActive(true);
	}

	public void HideManual()
	{
		manualPanel.SetActive(false);
	}
}
