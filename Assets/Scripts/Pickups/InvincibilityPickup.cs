using UnityEngine;

public class InvincibilityPickup : Pickup
{
    [SerializeField] private float duration = 5.0f;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.EnableInvincibility(duration);
        }
    }
}
