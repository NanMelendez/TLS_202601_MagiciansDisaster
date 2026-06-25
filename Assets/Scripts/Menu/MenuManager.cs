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
		Application.Quit();
	}

	public void Play()
	{
		SceneManager.LoadScene("base");
	}
}
