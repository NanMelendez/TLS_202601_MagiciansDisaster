using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyInstance;
	public float spawnCooldown;
	public int enemyLimit;
	public float spawnRadius = 1.0f;

	private float spawnTimer;
	private int currentEnemyCount;

	private void Awake()
	{
		currentEnemyCount = 0;
		spawnTimer = spawnCooldown;
	}

	private void Update()
	{
		if (spawnCooldown > 0.0f && enemyLimit >= 0)
		{
			if (spawnTimer > 0.0f)
				spawnTimer -= Time.deltaTime;
			else if (currentEnemyCount < enemyLimit || enemyLimit == -1)
			{
				spawnTimer = spawnCooldown;
				Summon();
			}
		}
	}

	private void OnDrawGizmos()
	{
		UnityEditor.Handles.color = Color.limeGreen;
		UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, spawnRadius);
	}

	public void Summon()
	{
		float distance = Random.Range(0.0f, spawnRadius);
		Vector2 direction = Quaternion.AngleAxis(Random.Range(-180.0f, 180.0f), Vector3.forward) * Vector2.right;

		GameObject entity = Instantiate(enemyInstance, transform.position + (Vector3)direction * distance, Quaternion.identity);
		entity.GetComponent<EnemyController>().spawner = this;
		currentEnemyCount++;
	}

	public void EnemyDeathSignal(float delay = 0.0f)
	{
		currentEnemyCount--;
		spawnTimer += delay;
	}
}
