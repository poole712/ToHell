using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f; // Set appropriate damage value

    private bool GoingForward = true;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    void Update()
    {
        if (GoingForward)
        {
            rb.velocity = Vector2.left * speed;
        }
        else
        {
            rb.velocity = Vector2.right * speed * 5;
        }
    }



    public void FlipBall()
    {
        GoingForward = false;
        GetComponent<SpriteRenderer>().flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
            gameObject.SetActive(false);
        }

    }
}
