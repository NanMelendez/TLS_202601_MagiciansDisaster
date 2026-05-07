using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int damage;
    [SerializeField]
    private SpriteRenderer enemySprite;
    [SerializeField]
    private Color colorOnHit;
    [SerializeField]
    private float damagedCooldown;
    [SerializeField]
    private EnemyKnockback eKnockback;
    private EnemyMovement movement;
    [NonSerialized]
    public EnemySpawner spawner;

    private int health;
    private Color defaultColor;
    private float damagedTimer;
    private bool deathSignalSent;

    void Awake()
    {
        maxHealth = Mathf.Max(maxHealth, 1);
        health = maxHealth;
        movement = gameObject.GetComponent<EnemyMovement>();
        defaultColor = enemySprite.color;
        damagedTimer = 0.0f;
        deathSignalSent = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (damagedTimer <= 0.0f)
        {
            if (collision.CompareTag("MeleeArea"))
            {
                int dmg = collision.gameObject.GetComponent<MeleeArea>().damage;
                TakeDamage(dmg);
                eKnockback.Knockback(collision.transform, 30);
                // Debug.Log($"Salud enemigo: {health}");
            }
            if (collision.CompareTag("Pellet"))
            {
                int dmg = collision.gameObject.GetComponent<Pellet>().damage;
                TakeDamage(dmg);
                eKnockback.Knockback(collision.transform, 15);
                // Debug.Log($"Salud enemigo: {health}");
            }
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            movement.shouldStopMoving = true;
            Destroy(gameObject, 0.5f);
            if (spawner && !deathSignalSent)
            {
                spawner.EnemyDeathSignal();
                deathSignalSent = true;
            }
        }

        if (damagedTimer > 0.0f)
            damagedTimer -= Time.deltaTime;
    }

    IEnumerator FlashOnDamage()
    {
        enemySprite.color = colorOnHit;

        yield return new WaitForSeconds(0.25f);

        enemySprite.color = defaultColor;
    }

    public void TakeDamage(int dmg)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);
        damagedTimer = damagedCooldown;
        StartCoroutine(FlashOnDamage());
    }

    public int Health
    {
        get { return health; }
    }

    public int Damage
    {
        get { return damage; }
    }
}
