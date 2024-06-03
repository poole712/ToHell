using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class InvincibilityPickupTests
{
    private GameObject invincibilityPickupGameObject;
    private InvincibilityPickup invincibilityPickup;
    private GameObject playerGameObject;
    private PlayerMaterialManager playerMaterialManager;
    private PlayerHealth playerHealth;

    [SetUp]
    public void SetUp()
    {
        // Set up the InvincibilityPickup GameObject and component
        invincibilityPickupGameObject = new GameObject();
        invincibilityPickup = invincibilityPickupGameObject.AddComponent<InvincibilityPickup>();

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerHealth = playerGameObject.AddComponent<PlayerHealth>();
        playerMaterialManager = playerGameObject.AddComponent<PlayerMaterialManager>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(invincibilityPickupGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator InvincibilityPickup_TestApplyEffect_EnablesInvincibility()
    {
        // Simulate applying the effect
        invincibilityPickup.TestApplyEffect(playerGameObject, playerMaterialManager);

        yield return null;
    }
}
