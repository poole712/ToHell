using UnityEngine;

public class Enemy : GameEntities
{
    public float Damage = 5.0f;  // Damage dealt by the enemy
    public ParticleSystem DestroyParticle;
    public GameObject Coin;

    protected override void OnTriggerEnter2D(Collider2D other)
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
            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            int cointAmount = Random.Range(0, 4);
            for (int i = 0; i < cointAmount; i++)
            {
                GameObject coin = Instantiate(Coin, new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(1, 5), Random.Range(2, 5)), ForceMode2D.Impulse);
            }

        }
    }
}
