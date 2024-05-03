using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float speed = 5.0f;
    public bool moveVertical = false;
    private Vector2 direction = Vector2.left;

    void Update()
    {
        if (moveVertical)
            transform.Translate(Vector2.up * Mathf.Cos(Time.timeSinceLevelLoad) * speed * Time.deltaTime);
        else
            transform.Translate(direction * speed * Time.deltaTime);
    }
}

