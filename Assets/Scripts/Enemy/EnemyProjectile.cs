using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;

    private int damage;
    private float knockback;

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public float Knockback
    {
        get
        {
            return knockback;
        }
    }

    public void Init(int damage, float knockback, Vector2 direction, float speed)
    {
        this.damage = damage;
        this.knockback = knockback;
        rb2d.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
