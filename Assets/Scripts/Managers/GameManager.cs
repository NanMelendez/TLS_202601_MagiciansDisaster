using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private CinemachineCamera cmCam;
	[SerializeField] private GameUIManager UIManager;
	public GameplayState state = GameplayState.Playing;

	public int Puntuacion = 0;

	private void Awake()
	{
		Time.timeScale = 1f;
		UIManager.State = state;
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
			}
		}
	}
}
