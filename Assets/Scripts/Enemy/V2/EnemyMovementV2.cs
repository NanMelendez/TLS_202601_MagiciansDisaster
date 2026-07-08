using System;
using UnityEngine;

public class EnemyMovementV2 : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;
    public EnemyFOVV2 fov;

    [NonSerialized] public float speed;
    private EnemyControllerV2 controller;
    private Vector2 direction;
    private float directionChangeCooldown = 0.0f;
    private EnemyActionSpot actionOnSpot;

	private static readonly int idleHash = Animator.StringToHash("isIdle");

	public Vector2 Direction
    {
        get { return direction; }
    }

    public Vector2 Target
    {
        get
        {
            return fov.TargetLocation;
        }
    }

    private void Awake()
    {
        direction = new Vector2(0.0f, 1.0f);
    }

    private void Update()
    {
        if (!fov.CanSeePlayer)
            HandleRandomDirection();

        if (controller != null)
        {
            HandlePlayerTargeted();
            HandleAttack();
        }
    }

    private void FixedUpdate()
    {
        if (controller != null)
            if (controller.state != EnemyState.KNOCKBACK)
                SetVelocity();
    }

    public void Init(EnemyControllerV2 controller, EnemyActionSpot actionOnSpot, float speed)
    {
        this.controller = controller;
        this.actionOnSpot = actionOnSpot;
        this.speed = speed;
        fov.Init(controller.data.visionAngle, controller.data.visionRadius);
    }

    public void StopVelocity()
    {
        rb2d.linearVelocity = Vector2.zero;
    }

    private void HandleRandomDirection()
    {
        directionChangeCooldown -= Time.deltaTime;
        if (directionChangeCooldown <= 0.0f)
        {
            float changeAngle = UnityEngine.Random.Range(-90.0f, 90.0f);
            Quaternion rotation = Quaternion.AngleAxis(changeAngle, transform.forward);
            direction = rotation * direction;
            fov.AimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            directionChangeCooldown = UnityEngine.Random.Range(1.0f, 5.0f);
        }
    }

    private void HandlePlayerTargeted()
    {
        if (fov.CanSeePlayer)
        {
            direction = Vector2.MoveTowards(direction, fov.Direction, fov.turnSpeed * Time.deltaTime);

            switch (actionOnSpot)
            {
                case EnemyActionSpot.Retreat:
                    direction *= -1.0f;
                    controller.state = EnemyState.FLEEING;
					controller.animator.SetBool(idleHash, false);
					break;
                default:
                case EnemyActionSpot.Follow:
                    controller.state = EnemyState.CHASING;
					controller.animator.SetBool(idleHash, false);
					break;
            }
        }
        else
        {
            controller.state = EnemyState.IDLE;
            controller.animator.SetBool(idleHash, true);
        }
    }

    private void HandleAttack()
    {
        if (fov.CanSeePlayer && controller.attacker.attackType == EnemyAtkType.Ranged)
        {
            controller.attacker.Fire(fov.TargetLocation - (Vector2)transform.position, 0.5f);
        }
    }

    private void SetVelocity()
    {
        rb2d.linearVelocity = direction * speed * (controller.state == EnemyState.IDLE ? 0.25f : 1.0f);
    }
}
