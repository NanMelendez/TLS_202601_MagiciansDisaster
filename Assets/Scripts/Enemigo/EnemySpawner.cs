using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyInstance;
    [SerializeField]
    private Transform playerTransform;

    public float spawnInterval;
    public int enemyLimit;
    private float spawnTimer;
    private int currentEnemyCount;

    void Awake()
    {
        currentEnemyCount = 0;
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        if (spawnInterval > 0 && enemyLimit >= 0)
        {
            if (spawnTimer > 0)
                spawnTimer -= Time.deltaTime;
            else if (currentEnemyCount < enemyLimit || enemyLimit == -1)
            {
                spawnTimer = spawnInterval;
                Summon();
                Debug.Log($"Conteo de enemigos: {currentEnemyCount}");
            }
        }
    }

    public void Summon()
    {
        GameObject go = Instantiate(enemyInstance, transform.position, Quaternion.identity);
        go.GetComponent<EnemyMovement>().playerTransform = playerTransform;
        go.GetComponent<EnemyHealth>().spawner = this;
        currentEnemyCount++;
    }

    public void EnemyDeathSignal()
    {
        currentEnemyCount--;
    }
}
