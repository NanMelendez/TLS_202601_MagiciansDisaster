using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerMovement movement;
	[SerializeField] private PlayerAim aim;
	[SerializeField] private Health health;
	[SerializeField] private Knockback knockback;
	[SerializeField] private PlayerMana mana;
	[SerializeField] private FlashController flashController;
	[SerializeField] private float destroyAfterSeconds;

	private bool isAlive;
	private int points;

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

    
}
