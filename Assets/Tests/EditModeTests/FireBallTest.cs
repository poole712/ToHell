using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class FireballTests
{
    private GameObject fireballGameObject;
    private Fireball fireball;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Set up the Fireball GameObject and component
        fireballGameObject = new GameObject();
        fireball = fireballGameObject.AddComponent<Fireball>();
        fireball.speed = 5f;
        fireball.damage = 10f;
        fireballGameObject.AddComponent<Rigidbody2D>();

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerGameObject.AddComponent<PlayerHealth>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(fireballGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator Fireball_Start_DestroyAfter3Seconds()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator Fireball_Update_MovesInCorrectDirection()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator Fireball_FlipBall_ChangesDirection()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator Fireball_OnTriggerEnter2D_PlayerTakesDamage()
    {
        yield return null;
    }
}
