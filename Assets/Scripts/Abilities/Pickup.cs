using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected abstract void ApplyEffect(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyEffect(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
