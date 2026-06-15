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
	[SerializeField] private FlashController flashController;
	[SerializeField] private PlayerAbilityHotbar hotbar;
	[SerializeField] private float destroyAfterSeconds;
	[SerializeField] private Animator spriteAnimator;


	[NonSerialized] public GameplayState state;

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
		//UIHandler.UpdateHealth(health.CurrentHealth, health.MaxHealth);
		//UIHandler.UpdateMana(mana.CurrentMana, mana.MaxMana);
    }

	private void Update()
	{
		if (!health.IsAlive && isAlive)
		{
			Destroy(gameObject, destroyAfterSeconds);
			isAlive = false;
		}

		CheckState();

		HandleAnimator();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
			health.TakeDamage(ec.contactDamage);
			//UIHandler.UpdateHealth(health.CurrentHealth, health.MaxHealth);
			StartCoroutine(AllowKnocback(health.HurtCooldown));
			Invoke(nameof(ApplyKnockback), 0.02f);
			flashController.Flash();

			Debug.Log($"¡TMRE ME HIRIERON, -{ec.contactDamage} PINCHES PUNTOS!");
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

	IEnumerator AllowKnocback(float seconds)
	{
		movement.enabled = false;
		Debug.Log("Movimiento deshabilitado [JUGADOR]");
		yield return new WaitForSeconds(seconds);
		movement.enabled = true;
		Debug.Log("Movimiento habilitado [JUGADOR]");
	}

	void ApplyKnockback()
	{
		knockback.Execute(movement.Direction, 50.0f);
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
