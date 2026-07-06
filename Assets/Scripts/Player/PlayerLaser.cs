using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [Min(0.01f)] public float laserRange = 0.01f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private AttackAnimation atkAnimation;
    [SerializeField] private CinemachineImpulseSource impulseSrc;

    private int damage;
    private float knockback;
    private Vector2 direction;

    [NonSerialized] public AttackEffectType effectType;
    [NonSerialized] public float effectDuration;
    [NonSerialized] public bool isHoldingAttack = true;

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
        if (!isHoldingAttack)
        {
            Destroy(gameObject);
        }
    }

    public void Init(int damage, float knockForce, Vector2 direction, float effectDuration, bool charged, AttackEffectType effectType)
    {
        if (charged)
            transform.localScale = Vector3.one * 2.5f;
        this.damage = damage;
        this.knockback = knockForce;
        this.effectType = effectType;
        this.effectDuration = effectDuration;
        this.direction = direction;

        LaserStuff();
    }

    private void LaserStuff()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, laserRange, wallLayer);

        lineRenderer.SetPosition(0, transform.position);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyControllerV2>().TakeLaserDamage(damage, knockback, 0.05f, Color.red);
            }

            impulseSrc.GenerateImpulseAt(hit.point, Vector3.one * knockback / 7.5f);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + (Vector3)direction * laserRange);
        }
    }
}
