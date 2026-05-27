using UnityEngine;

public class PlayerMana : MonoBehaviour
{
	[SerializeField] private int maxMana;

	private int currentMana;

	public int MaxMana
	{
		get { return maxMana; }
	}

	public int CurrentMana
	{
		get { return currentMana; }
	}

	public void ConsumeMana(int amount)
	{
		currentMana = Mathf.Max(currentMana - amount, 0);
	}

	public void AcquireMana(int amount)
	{
		currentMana = Mathf.Min(currentMana + amount, maxMana);
	}

	private void Awake()
	{
		maxMana = Mathf.Max(maxMana, 1);
		currentMana = maxMana;
	}
}
