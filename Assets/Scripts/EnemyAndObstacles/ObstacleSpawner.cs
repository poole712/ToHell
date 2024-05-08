using UnityEngine;

public class ObstacleSpawner : EntitiesSpawner
{
    protected override void SpawnEntity()
    {
        // Example of customizing spawn behavior, like randomizing the spawn position along Y-axis
        float randomY = Random.Range(-5f, 5f);  // Randomize the Y position within a range
        Vector2 finalSpawnPosition = new Vector2(spawnPosition.x, spawnPosition.y + randomY);

        // Instantiate the obstacle at the calculated position
        Instantiate(entityPrefab, finalSpawnPosition, Quaternion.identity);
    }
}
