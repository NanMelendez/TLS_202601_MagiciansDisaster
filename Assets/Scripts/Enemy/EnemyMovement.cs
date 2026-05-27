using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public float speed;
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private EnemyFOV fov;
	[SerializeField] private EnemyController controller;

	private Vector2 direction;
	private float directionChangeCooldown = 0.0f;

	private void Awake()
	{
		direction = new Vector2(0.0f, 1.0f);
	}

	private void OnDisable()
	{
		rb2d.linearVelocity = Vector2.zero;
	}

	private void Update()
	{
		if (!fov.CanSeePlayer)
			HandleRandomDirection();
		HandlePlayerTargeted();
	}

	private void FixedUpdate()
	{
		if (controller.state != EnemyState.KNOCKBACK)
			SetVelocity();
	}

	public Vector2 Direction
	{
		get { return direction; }
	}

	private void HandleRandomDirection()
	{
		directionChangeCooldown -= Time.deltaTime;
		if (directionChangeCooldown <= 0.0f)
		{
			float changeAngle = Random.Range(-90.0f, 90.0f);
			Quaternion rotation = Quaternion.AngleAxis(changeAngle, transform.forward);
			direction = rotation * direction;
			fov.AimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			directionChangeCooldown = Random.Range(1.0f, 5.0f);
		}
	}

	private void HandlePlayerTargeted()
	{
		if (fov.CanSeePlayer)
		{
			direction = Vector2.MoveTowards(direction, fov.Direction, fov.turnSpeed * Time.deltaTime);
			controller.state = EnemyState.CHASING;
		}
		else
		{
			controller.state = EnemyState.IDLE;
		}
	}

	private void SetVelocity()
	{
		rb2d.linearVelocity = direction * speed * (controller.state == EnemyState.IDLE ? 0.25f : 1.0f);
	}
}
