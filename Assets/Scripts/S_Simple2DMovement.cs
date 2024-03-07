using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Simple2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(rb2d.velocity.x < 5)
        {
            rb2d.AddForce(speed);
        }
    }
}
