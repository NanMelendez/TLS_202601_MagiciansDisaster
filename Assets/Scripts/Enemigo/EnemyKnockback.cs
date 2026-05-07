using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    public void Knockback(Transform playerTransform, float force)
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb2d.linearVelocity = direction * force;
    }
}
