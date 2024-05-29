using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f; // Set appropriate damage value

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
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
        else if (other.CompareTag("OutOfBounds"))
        {
            gameObject.SetActive(false);
        }
    }
}
