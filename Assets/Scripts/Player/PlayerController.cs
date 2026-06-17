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
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private BoxCollider2D playerCollider;
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
            EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
            health.TakeDamage(ec.contactDamage);
            StartCoroutine(AllowKnocback(collision.transform.position - transform.position, health.HurtCooldown, 50.0f));
            flashController.Flash();
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

	IEnumerator AllowKnocback(Vector2 direction, float seconds, float force)
	{
		movement.enabled = false;
		yield return new WaitForSeconds(0.02f);

        knockback.Execute(direction, force);

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
