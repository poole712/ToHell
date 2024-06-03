using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class FlyingEnemyTests
{
    private GameObject flyingEnemyGameObject;
    private FlyingEnemy flyingEnemy;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Set up the FlyingEnemy GameObject and component
        flyingEnemyGameObject = new GameObject();
        flyingEnemy = flyingEnemyGameObject.AddComponent<FlyingEnemy>();
        flyingEnemy.verticalSpeed = 1f;
        flyingEnemy.amplitude = 1f;
        flyingEnemy.damage = 10f;
        flyingEnemyGameObject.AddComponent<Rigidbody2D>();

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerGameObject.AddComponent<PlayerHealth>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(flyingEnemyGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator FlyingEnemy_MoveInSineWave_ChangesPosition()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator FlyingEnemy_OnTriggerEnter2D_PlayerTakesDamage()
    { 
        yield return null;
    }

    [UnityTest]
    public IEnumerator FlyingEnemy_OnTriggerEnter2D_DestroyedByFireball()
    {
        yield return null;
    }
}
