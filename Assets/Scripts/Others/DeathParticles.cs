using System.Collections;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private float duration;
	[SerializeField] private float delay;

	private void Awake()
	{
		StartCoroutine(Play());
	}

	private IEnumerator Play()
	{
		yield return new WaitForSeconds(delay);

		particles.Play();
		Destroy(gameObject, duration);
	}
}
