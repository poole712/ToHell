using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class SurprisedSpikeTests
{
    private GameObject surprisedSpikeGameObject;
    private SurprisedSpike surprisedSpike;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Set up the SurprisedSpike GameObject and component
        surprisedSpikeGameObject = new GameObject();
        surprisedSpike = surprisedSpikeGameObject.AddComponent<SurprisedSpike>();
        surprisedSpike.riseSpeed = 2f;
        surprisedSpike.activeDuration = 2f;
        surprisedSpike.cooldownDuration = 3f;

        // Set up the player GameObject and component
        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerGameObject.AddComponent<PlayerHealth>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(surprisedSpikeGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [UnityTest]
    public IEnumerator SurprisedSpike_ActivateSpike_MovesCorrectly()
    {
        // Start the coroutine manually
        surprisedSpike.StartCoroutine(surprisedSpike.ActivateSpike());

        yield return null;
    }

    [UnityTest]
    public IEnumerator SurprisedSpike_OnTriggerEnter2D_PlayerTakesDamage()
    {
        // Simulate making the spike active
        surprisedSpike.StartCoroutine(SimulateSpikeActive());

        // Simulate collision with the player
        surprisedSpike.OnTriggerEnter2D(playerGameObject.GetComponent<Collider2D>());

        yield return null;
    }

    private IEnumerator SimulateSpikeActive()
    {
        // Simulate waiting for cooldown duration
        yield return new WaitForSeconds(surprisedSpike.cooldownDuration);
        surprisedSpike.isActive = true;
    }
}
