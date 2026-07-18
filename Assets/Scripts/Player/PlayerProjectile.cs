using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private Collider2D c2d;
	[SerializeField] private AttackAnimation atkAnimation;
	[SerializeField] private float lifeBeforeDestroy;
	[SerializeField] private CinemachineImpulseSource impulseSrc;

	private int damage;
	private float knockback;

	private AudioClip hitSFX;

	private float spawnVolume = 0.5f;
	private float hitVolume = 0.5f;
	
	
	[NonSerialized] public AttackEffectType effectType;
	[NonSerialized] public float effectDuration;

	private void Awake()
	{
		impulseSrc.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy") || collision.CompareTag("Wall"))
		{
			SFXManager.instance.PlayClip(hitSFX, transform, hitVolume);
			StartCoroutine(blastBeforeDeath(lifeBeforeDestroy));
			impulseSrc.enabled = true;
			CameraShakeManager.instance.Shake(impulseSrc, knockback / 7.5f);
		}
    }

	public int Damage
	{
		get { return damage; }
	}

	public float Knockback
	{
		get { return knockback; }
	}

	public void Init(int damage, float knockback, float lifetime, Vector2 direction, float speed, float effectDuration, bool charged, AttackEffectType effectType, AudioClip spawnSFX, AudioClip hitSFX)
	{
		SFXManager.instance.PlayClip(spawnSFX, transform, spawnVolume);

		if (charged)
			transform.localScale = Vector3.one * 2.5f;

		this.damage = damage;
		this.knockback = knockback;
		this.effectType = effectType;
		this.effectDuration = effectDuration;
		this.hitSFX = hitSFX;

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
