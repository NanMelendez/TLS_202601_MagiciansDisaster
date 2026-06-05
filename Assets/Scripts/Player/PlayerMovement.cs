using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private InputActionReference movement;

	private Vector2 direction;
	private Key lastDirection;

	private void OnEnable()
	{
		movement.action.Enable();
		movement.action.performed += MovementCallback;
	}

	private void OnDisable()
	{
		movement.action.Disable();
		rb2d.linearVelocity = Vector2.zero;
		movement.action.performed -= MovementCallback;
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

	public Key LastKeyPressed
	{
		get { return lastDirection; }
	}

	public bool IsMoving
	{
		get { return direction != Vector2.zero; }
	}

	private void MovementCallback(InputAction.CallbackContext context)
	{
		switch (context.control.name)
		{
			case "w":
				lastDirection = Key.W;
				break;
			case "a":
				lastDirection = Key.A;
				break;
			case "s":
				lastDirection = Key.S;
				break;
			case "d":
				lastDirection = Key.D;
				break;
		}
	}
}
