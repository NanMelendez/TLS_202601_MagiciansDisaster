using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private InputActionReference movement;

	private Vector2 direction;

	private void OnEnable()
	{
		movement.action.Enable();
	}

	private void OnDisable()
	{
		movement.action.Disable();
		rb2d.linearVelocity = Vector2.zero;
	}

	private void Update()
	{
		direction = movement.action.ReadValue<Vector2>();
	}

	private void FixedUpdate()
	{
		rb2d.linearVelocity = direction * speed;
	}

	public Vector2 Direction
	{
		get { return direction; }
	}
}
