using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] protected int maxHealth;
	[SerializeField] private float hurtCooldown;
	[SerializeField] private AudioClip healSFX;
	[SerializeField] private AudioClip hurtSFX;

    protected int currentHealth;
	private float hurtCooldownTimer;

	public int MaxHealth
	{
		get {  return maxHealth; }
		set
		{
			maxHealth = Mathf.Max(value, 1);
			currentHealth = maxHealth;
		}
	}

	public int CurrentHealth
	{
		get { return currentHealth; }
		set
		{
			if (value < currentHealth && hurtSFX)
				SFXManager.instance.PlayClip(hurtSFX, transform, 1.0f);
			else if (value > currentHealth && healSFX)
				SFXManager.instance.PlayClip(healSFX, transform, 1.0f);

			currentHealth = Mathf.Clamp(value, 0, maxHealth);
		}
	}

	public bool IsAlive
	{
		get { return currentHealth > 0; }
	}

	public float HurtCooldown
	{
		get { return hurtCooldown; }
		set
		{
			hurtCooldown = Mathf.Max(value, 0.01f);
		}
	}

	public float Cooldown
	{
		get { return hurtCooldownTimer; }
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
			hurtCooldownTimer = Mathf.Max(hurtCooldownTimer - Time.deltaTime, 0.0f);
	}

	public virtual void TakeDamage(int damage)
	{
		if (hurtCooldownTimer > 0.0f)
			return;

		if (hurtSFX)
			SFXManager.instance.PlayClip(hurtSFX, transform, 1.0f);

		currentHealth = Mathf.Max(currentHealth - damage, 0);
		hurtCooldownTimer = hurtCooldown;
	}

	public virtual void Heal(int health)
	{
		if (healSFX)
			SFXManager.instance.PlayClip(healSFX, transform, 1.0f);

		currentHealth = Mathf.Min(currentHealth + health, maxHealth);
	}
}
