using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
	[SerializeField] private AudioSource sfxObject;

	public static SFXManager instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void PlayClip(AudioClip audioClip, Transform spawnTransform, float volume)
	{
		AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

		audioSource.clip = audioClip;
		audioSource.volume = volume;

		audioSource.Play();

		float clipLength = audioSource.clip.length;

		Destroy(audioSource.gameObject, clipLength);
	}

	public void PlayRandomClip(List<AudioClip> audioClips, Transform spawnTransform, float volume)
	{
		int rand = Random.Range(0, audioClips.Count);

		AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

		audioSource.clip = audioClips[rand];
		audioSource.volume = volume;

		audioSource.Play();

		float clipLength = audioSource.clip.length;

		Destroy(audioSource.gameObject, clipLength);
	}
}
