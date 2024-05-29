using UnityEngine;

public class SpikeSpawner : EntitiesSpawner
{
    protected override void SpawnEntity()
    {
        Vector2 spawnPos = new Vector2(spawnPosition.x, Random.Range(-3f, 3f)); // Randomize y position for variety
        Instantiate(entityPrefab, spawnPos, Quaternion.identity);
    }
}

