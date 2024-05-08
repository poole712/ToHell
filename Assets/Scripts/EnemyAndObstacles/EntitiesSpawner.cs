using UnityEngine;

public abstract class EntitiesSpawner : MonoBehaviour
{
    public GameObject entityPrefab;  // The prefab of the entity to spawn
    public float spawnRate = 2.0f;   // How frequently to spawn entities
    public Vector2 spawnPosition = new Vector2(10f, 0f);  // Default spawn position

    private float timer = 0f;        // Timer to track spawn intervals

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnEntity();
            timer = 0f;  // Reset the timer after spawning an entity
        }
    }

    protected virtual void SpawnEntity()
    {
        Instantiate(entityPrefab, spawnPosition, Quaternion.identity);
    }
}
