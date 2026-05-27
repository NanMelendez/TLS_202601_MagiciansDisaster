using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : Aim
{
	private void Update()
	{
		Vector2 mousePos = Mouse.current.position.ReadValue();
		position = Camera.main.ScreenToWorldPoint(mousePos);
		Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		direction = (mousePos - screenPos).normalized;
	}
}
