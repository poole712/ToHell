using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private int healthAmount = 20;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.IncreaseHealth(healthAmount);
        }
    }
}
