using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected abstract void ApplyEffect(GameObject player, PlayerMaterialManager playerMatMgr);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMaterialManager playerMatManager = collision.gameObject.GetComponent<PlayerMaterialManager>();
            ApplyEffect(collision.gameObject, playerMatManager);
            Destroy(gameObject);
        }
    }
}
