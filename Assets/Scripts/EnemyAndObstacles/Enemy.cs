using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 0.25f;  // Speed at which the enemy moves
    public float Damage = 5.0f;

    private Rigidbody2D _rb;
    private Camera _mainCamera;
    private Vector2 _offscreenSpawnPoint = new Vector2(10f, 0);  // Adjust as needed based on your camera setup

    private void Start()
    {
        _mainCamera = Camera.main;  // Get the main camera to calculate visibility
        _rb = GetComponent<Rigidbody2D>();
        InitializeMovement();
    }

    private void InitializeMovement()
    {
        _rb.velocity = Vector2.left * Speed;  // Move left
    }

    private void Update()
    {
        CheckOutOfScreen();
    }

    private void CheckOutOfScreen()
    {
        Vector3 screenPoint = _mainCamera.WorldToViewportPoint(transform.position);
        // If the enemy is no longer visible (out of the left side of the screen)
        if (screenPoint.x < -0.1)  // A little buffer to ensure it's fully off-screen
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy hit something");
        if (other.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<PlayerHealth>().Damage(Damage);
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
        Vector2 respawnPosition = new Vector2(_offscreenSpawnPoint.x, spawnY);
        transform.position = respawnPosition;
        gameObject.SetActive(true);  // Make enemy visible and active again
        InitializeMovement();  // Reinitialize the movement to ensure it continues moving left
    }
}
