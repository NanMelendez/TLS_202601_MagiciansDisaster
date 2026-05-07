using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private float maxHurtCooldown;
    private float hurtCooldown;

    void Awake()
    {
        maxHealth = Mathf.Max(maxHealth, 1);
        health = maxHealth;
        hurtCooldown = 0;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo") && hurtCooldown <= 0)
        {
            int dmg = collision.gameObject.transform.parent.gameObject.GetComponent<EnemyHealth>().Damage;
            TakeDamage(dmg);
            Debug.Log($"Salud jugador: {health}");
            hurtCooldown = maxHurtCooldown;
        }
    }

    void Update()
    {
        if (hurtCooldown > 0)
            hurtCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);
    }

    public int Health
    {
        get { return health; }
    }
}
