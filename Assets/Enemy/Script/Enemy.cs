using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;  // Speed at which the enemy moves
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 offscreenSpawnPoint = new Vector2(10f, 0);  // Adjust as needed based on your camera setup

    private void Start()
    {
        mainCamera = Camera.main;  // Get the main camera to calculate visibility
        rb = GetComponent<Rigidbody2D>();
        InitializeMovement();
    }

    private void InitializeMovement()
    {
        rb.velocity = Vector2.left * speed;  // Move left
    }

    private void Update()
    {
        CheckOutOfScreen();
    }

    private void CheckOutOfScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        // If the enemy is no longer visible (out of the left side of the screen)
        if (screenPoint.x < -0.1)  // A little buffer to ensure it's fully off-screen
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            //Destroy(other.gameObject);  // Simulate player death
            //Respawn();  // Also respawn this enemy
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(false);  // Make enemy invisible and inactive
        Invoke(nameof(Reactivate), 5f);  // Reactivate after 5 seconds
    }

    private void Reactivate()
    {
        float spawnY = Random.Range(-4f, 4f);  // Respawn at a random height within the camera view
        Vector2 respawnPosition = new Vector2(offscreenSpawnPoint.x, spawnY);
        transform.position = respawnPosition;
        gameObject.SetActive(true);  // Make enemy visible and active again
        InitializeMovement();  // Reinitialize the movement to ensure it continues moving left
    }
}
