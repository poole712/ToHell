using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private Vector2 speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(3, 0);
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        //Debug.Log(rb2d.velocity.x);
        if(rb2d.velocity.x < 3)
        {
            rb2d.AddForce(speed, ForceMode2D.Force);
        }
    }

}
