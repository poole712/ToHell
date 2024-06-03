using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class EnemyTests
{
    private GameObject enemyGameObject;
    private Enemy enemy;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Set up the enemy GameObject and component
        enemyGameObject = new GameObject();
        enemy = enemyGameObject.AddComponent<Enemy>();
        enemy.Damage = 5.0f;

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerGameObject.AddComponent<PlayerHealth>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(enemyGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator Enemy_OnDestroy_ParticlesAndCoinsAreSpawned()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator Enemy_OnTriggerEnter2D_PlayerTakesDamage()
    {

        yield return null;
    }
}
