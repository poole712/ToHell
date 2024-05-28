using UnityEngine;

public class SpeedPickup : Pickup
{
    [SerializeField] private float speedIncrease = 2.0f;
    [SerializeField] private float duration = 5.0f;

    protected override void ApplyEffect(GameObject player)
    {
        Player2DMovement playerMovement = player.GetComponent<Player2DMovement>();
        if (playerMovement != null)
        {
            playerMovement.IncreaseSpeed(speedIncrease, duration);
        }
    }
}
