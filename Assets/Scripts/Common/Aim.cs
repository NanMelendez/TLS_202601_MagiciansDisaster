using UnityEngine;

public class Aim : MonoBehaviour
{
	protected Vector2 direction;
	protected Vector2 position;

	private void Awake()
	{
		direction = new Vector2(0.0f, 1.0f);
	}

	public Vector2 Direction
	{
		get { return direction; }
	}

	public Vector2 Position
	{
		get { return position; }
	}
}
