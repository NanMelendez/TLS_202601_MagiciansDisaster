using Unity.Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private CinemachineCamera cmCam;

	private void Update()
	{
		if (player)
		{
			Health playerHealth = player.GetComponent<Health>();
			if (!playerHealth.IsAlive)
				cmCam.Follow = null;
		}
	}
}
