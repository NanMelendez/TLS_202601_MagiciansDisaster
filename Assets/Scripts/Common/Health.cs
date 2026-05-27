using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private float hurtCooldown;

	private int currentHealth;
	private float hurtCooldownTimer;

	public int MaxHealth
	{
		get {  return maxHealth; }
	}

	public int CurrentHealth
	{
		get { return currentHealth; }
	}

	public bool IsAlive
	{
		get { return currentHealth > 0; }
	}

	public float HurtCooldown
	{
		get { return hurtCooldown; }
	}

	private void Awake()
	{
		maxHealth = Mathf.Max(maxHealth, 1);
		currentHealth = maxHealth;
		hurtCooldownTimer = 0.0f;
	}

	private void Update()
	{
		if (hurtCooldownTimer > 0.0f)
			hurtCooldownTimer -= Time.deltaTime;
	}

	public void TakeDamage(int damage)
	{
		if (hurtCooldownTimer > 0.0f)
			return;

		currentHealth = Mathf.Max(currentHealth - damage, 0);
		hurtCooldownTimer = hurtCooldown;
	}

	public void Heal(int health)
	{
		currentHealth = Mathf.Min(currentHealth + health, maxHealth);
	}
}
