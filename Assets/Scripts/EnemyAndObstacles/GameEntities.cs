using UnityEngine;

public abstract class GameEntities : MonoBehaviour
{
    public float Speed = 0.25f;  // Default speed at which the game entity moves
    private Camera _mainCamera;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _mainCamera = Camera.main;  // Get the main camera to calculate visibility
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        InitializeMovement();
    }

    protected virtual void InitializeMovement()
    {
        _rb.velocity = Vector2.left * Speed;  // Move left by default
    }

    protected virtual void Update()
    {
        CheckOutOfScreen();
    }

    protected void CheckOutOfScreen()
    {
        Vector3 screenPoint = _mainCamera.WorldToViewportPoint(transform.position);
        if (screenPoint.x < -0.1)  // A little buffer to ensure it's fully off-screen
        {
            OnExitScreen();
        }
    }

    protected virtual void OnExitScreen()
    {
        gameObject.SetActive(false);  // Make the entity inactive
        Invoke(nameof(Respawn), 5f);  // Respawn the entity after 5 seconds
    }

    protected virtual void Respawn()
    {
        float spawnY = Random.Range(-4f, 4f);  // Respawn at a random height within the camera view
        transform.position = new Vector2(10f, spawnY);  // Reset the position offscreen to the right
        gameObject.SetActive(true);  // Make the entity active again
        InitializeMovement();  // Reinitialize the movement to ensure it continues moving left
    }

    // Add this virtual method
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Base implementation, if any general behavior is needed
    }
}
