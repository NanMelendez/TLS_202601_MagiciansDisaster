using System;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private Collider2D c2d;
	[SerializeField] private AttackAnimation atkAnimation;
	[SerializeField] private float lifeBeforeDestroy;

	private int damage;
	private float knockback;
	
	
	[NonSerialized] public AttackEffectType effectType;
	[NonSerialized] public float effectDuration;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy") || collision.CompareTag("Wall"))
            StartCoroutine(blastBeforeDeath(lifeBeforeDestroy));
    }

	public int Damage
	{
		get { return damage; }
	}

	public float Knockback
	{
		get { return knockback; }
	}

	public void Init(int damage, float knockForce, float lifetime, Vector2 direction, float speed, float effectDuration, bool charged, AttackEffectType effectType)
	{
		if (charged)
			transform.localScale = Vector3.one * 2.5f;
		this.damage = damage;
		this.knockback = knockForce;
		this.effectType = effectType;
		this.effectDuration = effectDuration;
		rb2d.linearVelocity = direction * speed;
		Destroy(gameObject, lifetime);
		atkAnimation.Init(charged);
	}

	IEnumerator blastBeforeDeath(float s)
	{
		c2d.enabled = false;
		rb2d.linearVelocity = Vector2.zero;
		atkAnimation.Blast();

		yield return new WaitForSeconds(s);
        
		Destroy(gameObject);
    }
}
