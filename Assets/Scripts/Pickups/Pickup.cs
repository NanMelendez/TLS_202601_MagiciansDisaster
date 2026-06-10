using UnityEngine;

public class Pickup : MonoBehaviour
{
	[SerializeField] private PickupData data;

	private PickupType type;
	private int points;

	private void Awake()
	{
		type = data.type;
		points = data.points;
	}

	public PickupType Type
	{
		get { return type; }
	}

	public int Points
	{
		get { return points; }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			PlayerController player = collision.GetComponent<PlayerController>();
			switch (type)
			{
				case PickupType.Coin:
					player.GetComponent<Score>().IncrementScore(points);
					break;
				case PickupType.Health:
					player.GetComponent<Health>().Heal(points);
					break;
				case PickupType.Mana:
					player.GetComponent<PlayerMana>().AcquireMana(points);
					break;
			}

			Destroy(gameObject);
		}
	}
}
