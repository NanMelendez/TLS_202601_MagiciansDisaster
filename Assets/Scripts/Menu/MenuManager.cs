using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject manual;

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
