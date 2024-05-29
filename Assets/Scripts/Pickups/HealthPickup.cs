using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private int healthAmount = 20;

    protected override void ApplyEffect(GameObject player, PlayerMaterialManager playerMatMgr)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerMatMgr.SetMaterial("Health", 3);
            playerHealth.IncreaseHealth(healthAmount);
        }
    }
}