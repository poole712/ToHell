using UnityEngine;

public class Enemy : GameEntities
{
    public float Damage = 5.0f;  // Damage dealt by the enemy
    public ParticleSystem DestroyParticle;
    public GameObject Coin;


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player
            other.gameObject.GetComponent<PlayerHealth>().Damage(Damage);
        }
    }

    protected void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        if (DestroyParticle != null)
        {
            ParticleSystem particleInstance = Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(particleInstance.gameObject, particleInstance.main.duration); // Destroy the particle system after its duration
        }

        if (Coin != null)
        {
            int coinAmount = Random.Range(0, 4);
            for (int i = 0; i < coinAmount; i++)
            {
                GameObject coin = Instantiate(Coin, new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
                Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(new Vector2(Random.Range(1, 5), Random.Range(2, 5)), ForceMode2D.Impulse);
                }
                Destroy(coin, 5.0f); // Destroy the coin after 5 seconds to prevent memory leak
            }
        }

    }
}
