using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class HealthPickupTests
{
    private GameObject healthPickupGameObject;
    private HealthPickup healthPickup;
    private GameObject playerGameObject;
    private PlayerMaterialManager playerMaterialManager;
    private PlayerHealth playerHealth;

    [SetUp]
    public void SetUp()
    {
        // Set up the HealthPickup GameObject and component
        healthPickupGameObject = new GameObject();
        healthPickup = healthPickupGameObject.AddComponent<HealthPickup>();

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerHealth = playerGameObject.AddComponent<PlayerHealth>();
        playerMaterialManager = playerGameObject.AddComponent<PlayerMaterialManager>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(healthPickupGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator HealthPickup_TestApplyEffect_IncreasesPlayerHealth()
    {
        // Simulate applying the effect
        healthPickup.TestApplyEffect(playerGameObject, playerMaterialManager);

        yield return null;
    }
}
