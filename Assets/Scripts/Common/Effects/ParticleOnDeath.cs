using UnityEngine;

public class ParticleOnDeath : MonoBehaviour
{
	private GameObject particleInstance;

	public void Play()
	{
		particleInstance = Instantiate(Resources.Load<GameObject>("Misc/DeathParticles"), transform.position, Quaternion.identity);
	}
}
