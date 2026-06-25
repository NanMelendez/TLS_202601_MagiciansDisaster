using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyFOV : Aim
{
	public float radius;
	[Range(1, 360)] public float angle = 45.0f;
	public LayerMask targetLayer;
	public LayerMask wallLayer;
	public float turnSpeed = 1.0f;
	[SerializeField] private Light2D visionLight;

	private GameObject playerRef;
	private bool canSeePlayer;
	private bool seesWall;
	private float aimAngle = 0.0f;

	private void Start()
	{
		playerRef = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(FOVCheck());
	}

	private void Update()
	{
		// Debug.Log($"Object: {gameObject.name}\nLooking at angle: {aimAngle}°\nFOV angle: {angle}°");

		if (canSeePlayer)
		{
			// Debug.Log("MAKE A TRADE!"); // Referencia random a SOUL
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

		Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z + aimAngle, -angle / 2.0f + 90);
		Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z + aimAngle, angle / 2.0f + 90);

		Gizmos.color = canSeePlayer ? Color.red : Color.yellow;
		Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
		Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);
	}

	public bool CanSeePlayer
	{
		get {  return canSeePlayer; }
	}

	public bool SeesWall
	{
		get { return seesWall; }
	}

	public float AimAngle
	{
		get
		{
			return aimAngle;
		}
		set
		{
			aimAngle = value;
			direction = Quaternion.AngleAxis(aimAngle, Vector3.forward) * Vector3.right;
		}
	}

	private IEnumerator FOVCheck()
	{
		// WaitForSeconds wait = new(0.2f);

		while (true)
		{
			yield return null;
			FOV();
		}
	}

	private void FOV()
	{
		visionLight.pointLightInnerAngle = angle;
		visionLight.pointLightOuterAngle = angle;
		visionLight.pointLightInnerRadius = radius * 3.0f / 4.0f;
		visionLight.pointLightOuterRadius = radius;

		visionLight.gameObject.GetComponent<Transform>().rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward) * Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f));

		Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

		if (rangeCheck.Length > 0)
		{
			Transform target = rangeCheck[0].transform;
			Vector2 direction = (target.position - transform.position).normalized;

			if (Vector2.Angle(this.direction, direction) < angle / 2)
			{
				float distance = Vector2.Distance(transform.position, target.position);

				if (!Physics2D.Raycast(transform.position, direction, distance, wallLayer))
				{
					canSeePlayer = true;
					this.direction = direction;
					this.position = target.position;
					aimAngle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
				}
				else
					canSeePlayer = false;
			}
			else
				canSeePlayer = false;
		}
		else if (canSeePlayer)
			canSeePlayer = false;

		Collider2D[] rangeWallCheck = Physics2D.OverlapCircleAll(transform.position, radius * 0.25f, wallLayer);

		if (rangeWallCheck.Length > 0)
		{
			Transform target = rangeWallCheck[0].transform;
			Vector2 direction = (target.position - transform.position).normalized;

			if (Vector2.Angle(this.direction, direction) < angle / 2)
				seesWall = true;
		}
		else if (seesWall)
		{
			seesWall = false;
		}
	}

	private Vector2 DirectionFromAngle(float localAngle, float globalAngle)
	{
		globalAngle -= localAngle;

		return new Vector2(Mathf.Sin(globalAngle * Mathf.Deg2Rad), Mathf.Cos(globalAngle * Mathf.Deg2Rad));
	}
}
