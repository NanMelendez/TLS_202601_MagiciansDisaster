using System;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;
	// [SerializeField] private Animator animator;
	[SerializeField] private AttackAnimation atkAnimation;

	private int damage;
	private float knockback;

	// private static readonly int AnimChargedHash = Animator.StringToHash("charged");

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Spawner") && !collision.CompareTag("Player") && !collision.CompareTag("PlayerProjectile"))
			Destroy(gameObject);
	}

	public int Damage
	{
		get { return damage; }
	}

	public float Knockback
	{
		get { return knockback; }
	}

	public void Init(int damage, float knockForce, float lifetime, Vector2 direction, float speed, bool charged)
	{
		if (charged)
			transform.localScale = Vector3.one * 2.5f;
		this.damage = damage;
		this.knockback = knockForce;
		rb2d.linearVelocity = direction * speed;
		Destroy(gameObject, lifetime);
		// animator.SetFloat(AnimChargedHash, charged ? 1.0f : 0.0f);
		atkAnimation.Init(charged);
	}
}
