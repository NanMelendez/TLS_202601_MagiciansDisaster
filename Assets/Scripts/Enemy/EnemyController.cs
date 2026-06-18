using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private EnemyMovement movement;
	[SerializeField] private Health health;
	[SerializeField] private Knockback knockback;
	[SerializeField] private FlashController flashController;
	[SerializeField] private EnemyHealthBarUI healthBarUI;
	[SerializeField] private float destroyAfterSeconds;
	public int contactDamage;

	[NonSerialized] public EnemySpawner spawner;
	[NonSerialized] public EnemyState state = EnemyState.IDLE;

	private bool isAlive;
	private Vector2 lastHitPos;

	private void Awake()
	{
		isAlive = true;
	}

	private void Update()
	{
		if (!health.IsAlive && isAlive)
		{
			Destroy(gameObject, destroyAfterSeconds);
			isAlive = false;
			movement.enabled = false;

			if (spawner)
				spawner.EnemyDeathSignal(destroyAfterSeconds + 1.0f);
		}

		// Debug.Log($"Enemigo: {name} | Velocidad: {GetComponent<Rigidbody2D>().linearVelocity}");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerProjectile"))
		{
			PlayerProjectile pp = collision.gameObject.GetComponent<PlayerProjectile>();
			lastHitPos = collision.transform.position;
			// TakeDamage(pp.Damage, pp.Knockback);

			switch (pp.effectType)
			{
				case AttackEffectType.Burn:
					StartCoroutine(ContinuousAttack(pp.Damage, pp.Knockback, 3.0f));
					break;
				case AttackEffectType.Stun:
					StartCoroutine(SlowdownMovement(0.0f, pp.effectDuration));
					TakeDamage(pp.Damage, pp.Knockback);
					break;
				case AttackEffectType.Slowdown:
                    StartCoroutine(SlowdownMovement(0.25f, pp.effectDuration));
                    TakeDamage(pp.Damage, pp.Knockback);
                    break;
				default:
				case AttackEffectType.None:
                    TakeDamage(pp.Damage, pp.Knockback);
                    break;
			}
		}
	}

	public void TakeDamage(int dmg, float knockback)
	{
		state = EnemyState.KNOCKBACK;
		health.TakeDamage(dmg);
		flashController.Flash();
		healthBarUI.UpdateUI(health.CurrentHealth, health.MaxHealth);
		StartCoroutine(AllowKnocback(health.HurtCooldown, knockback));
	}

	private IEnumerator AllowKnocback(float seconds, float knockback)
	{
		movement.enabled = false;

		yield return null;

		ApplyKnockback(lastHitPos, knockback);

		yield return new WaitForSeconds(seconds);

		movement.enabled = true;
		state = EnemyState.IDLE;
	}

	private void ApplyKnockback(Vector2 hitPos, float force)
	{
		knockback.Execute(((Vector2)transform.position - hitPos).normalized, force);
	}

	public IEnumerator ContinuousAttack(int damage, float knockForce, float s)
	{
		float elapsed = 0.0f;

		bool firstKnock = false;

		while (elapsed < s)
		{
			elapsed += Time.deltaTime;
			if (!firstKnock)
			{
				TakeDamage(damage, knockForce);
				firstKnock = true;
			}
			else
				TakeDamage(damage, 0.0f);

			yield return null;
		}
	}

	public IEnumerator SlowdownMovement(float factor, float s)
	{
		float elapsed = 0.0f;
		float originalSpeed = movement.speed;

        movement.speed *= factor;

        while (elapsed < s)
		{
			elapsed += Time.deltaTime;

			yield return null;
		}

		movement.speed = originalSpeed;
	}
}
