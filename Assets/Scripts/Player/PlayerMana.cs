using UnityEngine;

public class PlayerMana : MonoBehaviour
{
	[SerializeField] private int maxMana;
	[SerializeField] private UIStatsHandler statsUI;
	[SerializeField] private AudioClip manaSFX;

	private int currentMana;

	public int MaxMana
	{
		get { return maxMana; }
	}

	public int CurrentMana
	{
		get { return currentMana; }
	}

    private void Awake()
    {
        maxMana = Mathf.Max(maxMana, 1);
        currentMana = maxMana;
		statsUI.UpdateMana(currentMana, maxMana);
    }

    public void ConsumeMana(int amount)
	{
		currentMana = Mathf.Max(currentMana - amount, 0);
		statsUI.UpdateMana(currentMana, maxMana);
	}

	public void AcquireMana(int amount)
	{
		if (manaSFX)
			SFXManager.instance.PlayClip(manaSFX, transform, 1.0f);

		currentMana = Mathf.Min(currentMana + amount, maxMana);
        statsUI.UpdateMana(currentMana, maxMana);
    }
}
