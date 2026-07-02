using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
	[SerializeField] private List<GameObject> lootInstances = new();
	[SerializeField] [Min(1)] private int maxQuantity = 1;
	[SerializeField] [Min(0.01f)] private float radius;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.purple;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	public void Drop()
	{
		for (int i = 0; i < maxQuantity; i++)
		{
			int index = Random.Range(0, lootInstances.Count);
			Instantiate(lootInstances[index], transform.position + (Vector3)Random.insideUnitCircle * radius, Quaternion.identity);
		}
	}
}
