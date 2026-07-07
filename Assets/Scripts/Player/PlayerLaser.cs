using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private CinemachineImpulseSource impulseSrc;

    private int damage;
    private float knockback;
    private bool startedAttack = false;

    private AudioClip hitSFX;
    private bool firsHit = false;

    [NonSerialized] public AttackEffectType effectType;
    [NonSerialized] public float effectDuration;
    [NonSerialized] public PlayerAbilityHotbar atkHotbar;
    [NonSerialized] public PlayerAim pAim;
	[NonSerialized] public float laserRange = 0.01f;

	public int Damage
    {
        get { return damage; }
    }

    public float Knockback
    {
        get { return knockback; }
    }

    private void Update()
    {
        if (!atkHotbar.IsAttacking)
        {
            Destroy(gameObject);
        }

        if (startedAttack)
        {
            LaserStuff();
		}
    }

    public void Init(int damage, float knockback, PlayerAim pAim, float laserRange, float effectDuration, bool charged, AttackEffectType effectType, PlayerAbilityHotbar atkHotbar, AudioClip spawnSFX, AudioClip hitSFX)
    {
        SFXManager.instance.PlayClip(spawnSFX, transform, 1.0f);

        if (charged)
            transform.localScale = Vector3.one * 2.5f;

        this.damage = damage;
        this.knockback = knockback;
        this.effectType = effectType;
        this.effectDuration = effectDuration;
        this.pAim = pAim;
        this.atkHotbar = atkHotbar;
        this.laserRange = laserRange;
        this.hitSFX = hitSFX;

        startedAttack = true;

        SFXManager.instance.PlayClip(spawnSFX, transform, 1.0f);
    }

    private void LaserStuff()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, pAim.Direction, laserRange, wallLayer);

        lineRenderer.SetPosition(0, transform.position);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyControllerV2>().TakeLaserDamage(damage, knockback, 0.05f, Color.red);
            }

            impulseSrc.GenerateImpulseAt(hit.point, Vector3.one * 0.1f);

            if (!firsHit)
            {
                firsHit = true;
                SFXManager.instance.PlayClip(hitSFX, transform, 1.0f);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + (Vector3)pAim.Direction * laserRange);
        }
    }
}
