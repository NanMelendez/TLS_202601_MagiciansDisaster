using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
	[SerializeField] private List<GameObject> instances;
	[SerializeField] private float spawnInterval;
	[SerializeField] private int spawnLimit;
	[SerializeField] private float radius = 1.0f;
	[SerializeField] private Transform anchor;

	private float spawnTimer;
	private int currentPickupCount;

	private void Awake()
	{
		currentPickupCount = 0;
		spawnTimer = spawnInterval;
	}

	private void Update()
	{
		if (spawnInterval > 0.0f && spawnLimit >= 0)
		{
			if (spawnTimer > 0.0f)
				spawnTimer -= Time.deltaTime;
			else if (currentPickupCount < spawnLimit || spawnLimit == -1)
			{
				spawnTimer = spawnInterval;
				Summon();
			}
		}
	}

	private void OnDrawGizmos()
	{
		UnityEditor.Handles.color = Color.limeGreen;
		UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
	}

	public void PickedUpSignal(float delay)
	{
		currentPickupCount--;

	}

	public void Summon()
	{
		float distance = Random.Range(0.0f, radius);
		Vector2 direction = Quaternion.AngleAxis(Random.Range(-180.0f, 180.0f), Vector3.forward) * Vector2.right;

		GameObject entity = Instantiate(instances[Random.Range(0, instances.Count - 1)], transform.position + (Vector3)direction * distance, Quaternion.identity);
		entity.transform.parent = anchor;
		entity.GetComponent<Pickup>().spawner = this;
		currentPickupCount++;
	}
}
