using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private PlayerAim aim;
	[SerializeField] private Health health;
	[SerializeField] private Score score;
	[SerializeField] private Knockback knockback;
	[SerializeField] private PlayerMana mana;
	[SerializeField] private FlashController flash;
	[SerializeField] private PlayerAbilityHotbar hotbar;
	[SerializeField] private float destroyAfterSeconds;
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private BoxCollider2D playerCollider;
	[SerializeField] private Animator spriteAnimator;


	[NonSerialized] public GameplayState state;

	private Vector2 lastHitPos;

	private bool isAlive;

	private bool prevMovementState = false;
	private bool prevAttackState = false;

	private static readonly int AnimIdleHash = Animator.StringToHash("idle");
	private static readonly int AnimMovXHash = Animator.StringToHash("movX");
	private static readonly int AnimMovYHash = Animator.StringToHash("movY");
	private static readonly int AnimAttackingHash = Animator.StringToHash("attacking");

	private void Awake()
	{
		isAlive = true;
    }

	private void Update()
	{
		if (!health.IsAlive && isAlive)
		{
			rb2d.linearVelocity = Vector2.zero;
			playerCollider.enabled = false;
			Destroy(gameObject, destroyAfterSeconds);
			isAlive = false;
		}

		CheckState();

		HandleAnimator();
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
			if (health.Cooldown == 0.0f)
			{
				EnemyAttacker eAtk = collision.gameObject.GetComponent<EnemyAttacker>();

                Debug.Log($"Attacked! -{eAtk.Damage} HP");

                // EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
                lastHitPos = collision.transform.position;

				TakeDamage(eAtk.Damage, 25.0f, 0.25f, Color.red);
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            if (health.Cooldown == 0.0f)
            {
                EnemyProjectile eProj = collision.gameObject.GetComponent<EnemyProjectile>();
                lastHitPos = collision.transform.position;

                TakeDamage(eProj.Damage, eProj.Knockback, 0.25f, Color.red);
            }
        }
    }

    private void CheckState()
	{
		switch (state)
		{
			case GameplayState.Playing:
				movement.enabled = true;
				aim.enabled = true;
				hotbar.enabled = true;
				break;
			default:
				movement.enabled = false;
				aim.enabled = false;
				hotbar.enabled = false;
				break;
		}
	}

	public void TakeDamage(int dmg, float knockback, float flashTime, Color color)
	{
		health.TakeDamage(dmg);
		flash.Flash(color, flashTime);
		StartCoroutine(AllowKnocback(health.HurtCooldown, knockback));
	}

	private void ApplyKnockback(Vector2 hitPos, float force)
	{
		knockback.Execute(((Vector2)transform.position - hitPos).normalized, force);
	}

	IEnumerator AllowKnocback(float seconds, float knockback)
	{
		movement.enabled = false;
		yield return null;

        ApplyKnockback(lastHitPos, knockback);

        yield return new WaitForSeconds(seconds);
		movement.enabled = true;
	}

	private void HandleAnimator()
	{
		HandleAnimDirection();
		HandleAnimState();
	}

	private void HandleAnimDirection()
	{
		if (movement.IsMoving)
		{
			spriteAnimator.SetFloat(AnimMovXHash, movement.Direction.x);
			spriteAnimator.SetFloat(AnimMovYHash, movement.Direction.y);
		}
	}

	private void HandleAnimState()
	{
		if (movement.IsMoving != prevMovementState)
		{
			spriteAnimator.SetBool(AnimIdleHash, !movement.IsMoving);
			prevMovementState = movement.IsMoving;
		}

		if (hotbar.IsAttacking != prevAttackState)
		{
			spriteAnimator.SetBool(AnimAttackingHash, hotbar.IsAttacking);
			prevAttackState = hotbar.IsAttacking;
		}
	}
}
