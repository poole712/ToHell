using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacleManager : MonoBehaviour
{

    public List<GameObject> Enemies;
    public List<GameObject> Obstacles;
    public List<GameObject> Abilities;
    public GameObject Coin;
    public int Difficulty;

    private GameObject _player;
    private Player2DMovement _playerMovement;
    private SegmentManager _segmentManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        _playerMovement = FindFirstObjectByType<Player2DMovement>();
        _segmentManager = GetComponent<SegmentManager>();
        _player = _playerMovement.gameObject;
    }

    public void SpawnObstacleAndOrEnemy()
    {

        int spawnIndex = Random.Range(0, 3);
        switch (spawnIndex)
        {
            case 0:
                SpawnEnemy();
                SpawnCoinRandomly();
                break;
            case 1:
                SpawnObstacle();
                SpawnCoinRandomly();
                break;
            case 2:
                SpawnEnemy();
                SpawnObstacle();
                SpawnCoinRandomly();
                break;
        }
        if (Random.Range(0, 2) == 0)
        {
            for(int i = 0; i < Difficulty; i++)
            {
                StartCoroutine(DelayedSpawn());
            } 
        }
        int abilitySpawnProb = Random.Range(0, 5);
        if (abilitySpawnProb == 0)
        {
            SpawnAbility();
        }

    }
    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Instantiate(Enemies[Random.Range(0, Enemies.Count)], new Vector2(_player.transform.position.x + Random.Range(5.0f, 10.0f), _segmentManager.StartOffset.y + 0.1f), Quaternion.identity);
    }

    private void SpawnObstacle()
    {
        Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], new Vector2(_player.transform.position.x + Random.Range(5.0f, 10.0f), _segmentManager.StartOffset.y + 0.4f), Quaternion.identity);
    }

    private void SpawnCoinRandomly()
    {
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(Coin, new Vector2(_player.transform.position.x + Random.Range(5.0f, 10.0f), _segmentManager.StartOffset.y + 0.5f), Quaternion.identity);
            return;
        }
        Instantiate(Coin, new Vector2(_player.transform.position.x + Random.Range(5.0f, 10.0f), _segmentManager.StartOffset.y + 1.25f), Quaternion.identity);
    }

    private void SpawnAbility()
    {
        Instantiate(Abilities[Random.Range(0, Abilities.Count)], new Vector2(_player.transform.position.x + Random.Range(5.0f, 10.0f), _segmentManager.StartOffset.y + 0.5f), Quaternion.identity);

    }

}
