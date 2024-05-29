using UnityEngine;

public class FlyingEnemy : GameEntities
{
    public float verticalSpeed = 1f;
    public float amplitude = 1f;
    public float damage = 10f; // Damage dealt to the player on contact
    private Vector3 startPosition;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        // Make the enemy fly in a sine wave pattern
        float newY = Mathf.Sin(Time.time * verticalSpeed) * amplitude;
        transform.position = new Vector3(transform.position.x, startPosition.y + newY, transform.position.z);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
