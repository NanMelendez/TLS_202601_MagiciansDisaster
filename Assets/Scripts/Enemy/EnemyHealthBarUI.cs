using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
	[SerializeField] private Canvas canvas;
	[SerializeField] private Slider slider;

	private void Awake()
	{
		canvas.worldCamera = Camera.main;
	}

	public void UpdateUI(float currentHealth, float maxHealth)
	{
		slider.value = currentHealth / maxHealth;
	}
}
