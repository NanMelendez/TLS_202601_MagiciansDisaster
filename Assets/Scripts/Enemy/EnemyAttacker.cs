using System;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private GameObject projectileInstance;
    public float rangedInterval = 1.0f;

    private int damage;
    [NonSerialized] public AttackEffectType effectType;
    [NonSerialized] public EnemyAtkType attackType;
    private float rangedCooldown;

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    private void Update()
    {
        if (rangedCooldown > 0.0f)
            rangedCooldown -= Time.deltaTime;
    }

    public void Init(int damage, AttackEffectType effectType, EnemyAtkType attackType, float rangedInterval)
    {
        this.damage = damage;
        this.effectType = effectType;
        this.attackType = attackType;
        this.rangedInterval = rangedInterval;
        rangedCooldown = rangedInterval;
    }

    public void Fire(Vector2 direction, float speed)
    {
        if (rangedCooldown < 0.0f)
        {
            rangedCooldown = rangedInterval;

            if (projectileInstance)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                GameObject go = Instantiate(projectileInstance, transform.position, Quaternion.Euler(0.0f, 0.0f, angle));
                go.GetComponent<EnemyProjectile>().Init(damage, 30.0f, direction, speed);
            }
        }
    }
}
