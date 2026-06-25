using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerV2 : MonoBehaviour
{
    [SerializeField] private List<GameObject> instances = new List<GameObject>();
    public float cooldown;
    public int limit;
    public float radius = 1.0f;
    [SerializeField] private Transform anchor;

    private float spawnTimer;
    private int currentCount;

    private void Awake()
    {
        currentCount = 0;
        spawnTimer = cooldown;
    }

    private void Update()
    {
        if (cooldown > 0.0f && limit >= 0)
        {
            if (spawnTimer > 0.0f)
                spawnTimer -= Time.deltaTime;
            else if (currentCount < limit || limit == -1)
            {
                spawnTimer = cooldown;
                Summon();
            }
        }
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.limeGreen;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }

    public void Summon()
    {
        float distance = Random.Range(0.0f, radius);
        Vector2 direction = Quaternion.AngleAxis(Random.Range(-180.0f, 180.0f), Vector3.forward) * Vector2.right;

        GameObject entity = Instantiate(instances[Random.Range(0, instances.Count)], transform.position + (Vector3)direction * distance, Quaternion.identity);

        entity.transform.parent = anchor;
        entity.GetComponent<EnemyControllerV2>().Init(this);
        currentCount++;
    }

    public void EnemyDeathSignal(float delay = 0.0f)
    {
        currentCount--;
        spawnTimer = Mathf.Min(spawnTimer + delay, cooldown);
    }
}
