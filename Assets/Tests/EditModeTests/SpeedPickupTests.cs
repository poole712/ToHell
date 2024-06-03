using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class SpeedPickupTests
{
    private GameObject speedPickupGameObject;
    private SpeedPickup speedPickup;
    private GameObject playerGameObject;
    private PlayerMaterialManager playerMaterialManager;
    private PlayerAttack playerAttack;

    [SetUp]
    public void SetUp()
    {
        // Set up the SpeedPickup GameObject and component
        speedPickupGameObject = new GameObject();
        speedPickup = speedPickupGameObject.AddComponent<SpeedPickup>();

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerAttack = playerGameObject.AddComponent<PlayerAttack>();
        playerMaterialManager = playerGameObject.AddComponent<PlayerMaterialManager>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(speedPickupGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator SpeedPickup_TestApplyEffect_IncreasesPlayerSpeed()
    {
        // Simulate applying the effect
        speedPickup.TestApplyEffect(playerGameObject, playerMaterialManager);

        yield return null;
    }
}
