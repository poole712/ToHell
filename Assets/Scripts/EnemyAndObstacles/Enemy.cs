using UnityEngine;

public class Enemy : GameEntities
{
    public float Damage = 5.0f;  // Damage dealt by the enemy

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player
            other.gameObject.GetComponent<PlayerHealth>().Damage(Damage);
            Debug.Log("Enemy hit the player");
        }
    }
}
