using UnityEngine;

public class InvincibilityPickup : Pickup
{
    [SerializeField] private float duration = 5.0f;

    protected override void ApplyEffect(GameObject player, PlayerMaterialManager playerMatMgr)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerMatMgr.SetMaterial("Invincibility", duration);
            playerHealth.EnableInvincibility(duration);
        }
    }
}
