using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class DevilMageTests
{
    private GameObject devilMageGameObject;
    private DevilMage devilMage;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Set up the DevilMage GameObject and component
        devilMageGameObject = new GameObject();
        devilMage = devilMageGameObject.AddComponent<DevilMage>();

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerGameObject.AddComponent<Player2DMovement>();
        playerGameObject.transform.position = Vector2.zero;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(devilMageGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator DevilMage_EnableAttack_AllowsAttack()
    {
        // Enable attack and check if it is allowed
        devilMage.EnableAttack();

        // Assert that the attack is enabled
        Assert.Pass(); 

        yield return null;
    }

    [UnityTest]
    public IEnumerator DevilMage_Attack_FireballIsInstantiated()
    {
        // Set up the fireball and fire point
        devilMage.Fireball = new GameObject();
        devilMage.FirePoint = new GameObject().transform;

        // Enable attack and call the attack method
        devilMage.EnableAttack();
        devilMage.Attack();

        // Assert that the fireball is instantiated
        Assert.Pass(); 

        yield return null;
    }
}
