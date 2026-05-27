using System;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;

	private int damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("EnemyDetectionArea"))
			Destroy(gameObject);
	}

	public int Damage
	{
		get { return damage; }
	}

	public void Init(int damage, float lifetime, Vector2 direction, float speed)
	{
		this.damage = damage;
		rb2d.linearVelocity = direction * speed;
		Destroy(gameObject, lifetime);
	}
}
