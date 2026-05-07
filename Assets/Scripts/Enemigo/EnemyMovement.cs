using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 followDir = new Vector2(1.0f, 0.0f);
    [NonSerialized]
    public bool shouldStopMoving = false;

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerTransform)
            followDir = (playerTransform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (!shouldStopMoving)
            rb2d.linearVelocity = followDir * speed;
    }

    internal void ApllySlow(float slowFactor, float slowDuration)
    {
        throw new NotImplementedException();
    }
}
