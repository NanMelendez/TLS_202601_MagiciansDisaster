using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject manual;
	[SerializeField] private GameObject credits;

	private void Awake()
	{
		manual.SetActive(false);
	}

	public void ShowManual()
	{
		manual.SetActive(true);
	}

	public void HideManual()
	{
		manual.SetActive(false);
	}

	public void ShowCredits()
	{
		credits.SetActive(true);
	}

	public void HideCredits()
	{
		credits.SetActive(false);
	}

	public void Exit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void Play()
	{
		SceneManager.LoadScene("base");
	}
}
