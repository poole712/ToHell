using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacleManager : MonoBehaviour
{

    public List<GameObject> Enemies;
    public List<GameObject> Obstacles;

    private GameObject _player;
    private Player2DMovement _playerMovement;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _playerMovement = FindFirstObjectByType<Player2DMovement>();
        _player = _playerMovement.gameObject;
    }

    public void SpawnObstacleAndOrEnemy()
    {
        if (_playerMovement.InAir)
        {
            StartCoroutine(DelayedRespawn());
        }
        else
        {
            int spawnIndex = Random.Range(0, 2);
            switch (spawnIndex)
            {
                case 0:
                    Instantiate(Enemies[Random.Range(0, Enemies.Count - 1)], new Vector2(_player.transform.position.x + Random.Range(4, 6), _player.transform.position.y + 0.1f), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Obstacles[Random.Range(0, Obstacles.Count - 1)], new Vector2(_player.transform.position.x + Random.Range(4, 6), _player.transform.position.y + 0.25f), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Enemies[Random.Range(0, Enemies.Count - 1)], new Vector2(_player.transform.position.x + Random.Range(4, 6), _player.transform.position.y + 0.1f), Quaternion.identity);
                    Instantiate(Obstacles[Random.Range(0, Obstacles.Count - 1)], new Vector2(_player.transform.position.x + Random.Range(4, 6), _player.transform.position.y + 0.25f), Quaternion.identity);
                    break;
            }
        }

    }

    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(1);
        SpawnObstacleAndOrEnemy();
    }
}
