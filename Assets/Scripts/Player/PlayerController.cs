using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private PlayerAim aim;
	[SerializeField] private Health health;
	[SerializeField] private Knockback knockback;
	[SerializeField] private PlayerMana mana;
	[SerializeField] private FlashController flashController;
	[SerializeField] private PlayerAbilityHotbar hotbar;
	[SerializeField] private float destroyAfterSeconds;
	[SerializeField] private Animator spriteAnimator;

	private bool isAlive;
	private int points;

	private bool prevMovementState = false;

	private static readonly int AnimIdleHash = Animator.StringToHash("idle");
	private static readonly int AnimDirectionHash = Animator.StringToHash("direction");

	private float movX, movY;

	public int Points
	{
		get { return points; }
	}

	private void Awake()
	{
		isAlive = true;
		points = 0;
	}

	private void Update()
	{
		if (!health.IsAlive && isAlive)
		{
			Destroy(gameObject, destroyAfterSeconds);
			isAlive = false;
		}

		movX = Input.GetAxisRaw("Horizontal");
		movY = Input.GetAxisRaw("Vertical");

		spriteAnimator.SetFloat("MovX", movX);
		spriteAnimator.SetFloat("MovY", movY);

		HandleAnimator();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
			health.TakeDamage(ec.contactDamage);
			StartCoroutine(AllowKnocback(health.HurtCooldown));
			Invoke(nameof(ApplyKnockback), 0.02f);
			flashController.Flash();

			Debug.Log($"¡TMRE ME HIRIERON, -{ec.contactDamage} PINCHES PUNTOS!");
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

	public void AddPoints(int amount)
	{
		points += amount;
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
			switch (movement.LastKeyPressed)
			{
				case Key.S:
					spriteAnimator.SetInteger(AnimDirectionHash, 0);
					break;
				case Key.W:
					spriteAnimator.SetInteger(AnimDirectionHash, 1);
					break;
				case Key.A:
					spriteAnimator.SetInteger(AnimDirectionHash, 2);
					break;
				case Key.D:
					spriteAnimator.SetInteger(AnimDirectionHash, 3);
					break;
			}
		}
	}

	private void HandleAnimState()
	{
		if (movement.IsMoving != prevMovementState)
		{
			spriteAnimator.SetBool(AnimIdleHash, !movement.IsMoving);
			prevMovementState = movement.IsMoving;
		}
	}
}
