using UnityEngine;

public class SpeedPickup : Pickup
{
    [SerializeField] private float speedIncrease = 2.0f;
    [SerializeField] private float duration = 5.0f;

    protected override void ApplyEffect(GameObject player, PlayerMaterialManager playerMatMgr)
    {
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        if (playerAttack != null)
        {
            playerMatMgr.SetMaterial("Hammer Speed", duration);
            playerAttack.IncreaseSpeed(speedIncrease, duration);
        }
    }
}
