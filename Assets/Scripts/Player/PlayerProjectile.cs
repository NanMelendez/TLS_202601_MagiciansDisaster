using System;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private Animator animator;

	private int damage;

	private static readonly int AnimChargedHash = Animator.StringToHash("charged");

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Spawner"))
			Destroy(gameObject);
	}

	public int Damage
	{
		get { return damage; }
	}

	public void Init(int damage, float lifetime, Vector2 direction, float speed, bool charged)
	{
		if (charged)
			transform.localScale = Vector3.one * 2.5f;
		this.damage = damage;
		rb2d.linearVelocity = direction * speed;
		Destroy(gameObject, lifetime);
		animator.SetFloat(AnimChargedHash, charged ? 1.0f : 0.0f);
	}
}
