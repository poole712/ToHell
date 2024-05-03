using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2.0f;
    public Vector2 spawnPosition = new Vector2(10f, 0f);

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
