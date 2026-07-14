using System;
using System.Collections;
using UnityEngine;

public class EnemyControllerV2 : MonoBehaviour
{
    public EnemyData data;
    [SerializeField] private Health health;
    [SerializeField] private EnemyMovementV2 movement;
    [SerializeField] private Knockback knockback;
    public EnemyAttacker attacker;
    [SerializeField] private FlashController flash;
    [SerializeField] private EnemyHealthBarUI hbUI;
    [SerializeField] private EnemyLoot loot;
    public Animator animator;
	[SerializeField] private ParticleOnDeath deathParticles;

	private EnemyAtkType attackType;
    private AttackEffectType effectType;
    private EnemyActionSpot actionOnSpot;
    private bool isAlive = true;
    private Vector2 lastHitPos;
    private float destroyAfterSeconds;

    [NonSerialized] public EnemyState state = EnemyState.IDLE;
    [NonSerialized] public EnemySpawnerV2 spawner = null;

    private void Update()
    {
        if (!health.IsAlive && isAlive)
        {
            Destroy(gameObject, destroyAfterSeconds);
            isAlive = false;
            movement.StopVelocity();
            movement.enabled = false;

            if (spawner)
                spawner.EnemyDeathSignal(destroyAfterSeconds + 1.0f);

			deathParticles.Play();

			if (loot)
                loot.Drop();
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            if (health.Cooldown == 0.0f)
            {
                PlayerProjectile pp = collision.gameObject.GetComponent<PlayerProjectile>();
                lastHitPos = collision.transform.position;

                switch (pp.effectType)
                {
                    case AttackEffectType.Burn:
                        StartCoroutine(ContinuousAttack(pp.Damage, pp.Knockback, 3.0f, Color.orange));
                        break;
                    case AttackEffectType.Stun:
                        StartCoroutine(SlowdownMovement(0.0f, pp.effectDuration));
                        TakeDamage(pp.Damage, pp.Knockback, pp.effectDuration, Color.paleGreen);
                        break;
                    case AttackEffectType.Slowdown:
                        StartCoroutine(SlowdownMovement(0.25f, pp.effectDuration));
                        TakeDamage(pp.Damage, pp.Knockback, 0.2f, Color.red);
                        break;
                    default:
                    case AttackEffectType.None:
                        TakeDamage(pp.Damage, pp.Knockback, 0.2f, Color.red);
                        break;
                }
            }
        }
    }

	public void Init(EnemySpawnerV2 spawner = null)
    {
        this.spawner = spawner;

        health.MaxHealth = data.health;
        health.HurtCooldown = data.hurtCooldown;
        movement.Init(this, data.actionOnSpot, data.speed);
        attacker.Init(data.damage, data.effectType, data.attackType, data.rangedAttackInterval);
        destroyAfterSeconds = data.destroyAfterSeconds;
        attackType = data.attackType;
        effectType = data.effectType;
        actionOnSpot = data.actionOnSpot;
        flash.Flash(Color.lavenderBlush, 1.5f);
    }

    private void TakeDamage(int dmg, float knockback, float flashTime, Color color)
    {
        state = EnemyState.KNOCKBACK;
        health.TakeDamage(dmg);
        flash.Flash(color, flashTime);
        hbUI.UpdateUI(health.CurrentHealth, health.MaxHealth);
        StartCoroutine(AllowKnocback(health.HurtCooldown, knockback));
    }

    public void TakeLaserDamage(int dmg, float knockback, float flashTime, Color color)
    {
        TakeDamage(dmg, knockback, flashTime, color);
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

    public IEnumerator ContinuousAttack(int damage, float knockForce, float s, Color color)
    {
        float elapsed = 0.0f;

        bool firstKnock = false;

        while (elapsed < s)
        {
            elapsed += Time.deltaTime;
            if (health.Cooldown == 0.0f)
                if (!firstKnock)
                {
                    TakeDamage(damage, knockForce, 0.15f, Color.red);
                    firstKnock = true;
                }
                else
                    TakeDamage(damage, 0.0f, 0.15f, color);

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
