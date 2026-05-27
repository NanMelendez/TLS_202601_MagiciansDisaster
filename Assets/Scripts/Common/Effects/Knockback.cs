using UnityEngine;

public class Knockback : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;

	public void Execute(Vector2 direction, float force)
	{
		rb2d.AddForce(direction * force, ForceMode2D.Impulse);
	}
}
