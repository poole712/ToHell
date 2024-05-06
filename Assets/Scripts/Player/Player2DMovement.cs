using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [HideInInspector]public bool InAir;
    private Animator _animator;

    public Vector2 JumpHeight = new Vector2(1, 5f);
    public Vector2 Speed = new Vector2(5, 0);
    public S_SimpleCamera Camera;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(3, 0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !InAir)
        {
            _animator.SetTrigger("Jump");
            InAir = true;
            Debug.Log("Jumped");
            rb2d.AddForce(JumpHeight, ForceMode2D.Impulse);
            Camera.Jump();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Debug.Log(rb2d.velocity.x);
        if(rb2d.velocity.x < 3)
        {
            rb2d.AddForce(Speed, ForceMode2D.Force);
        }
        
    }

    public void Landed()
    {
        InAir = false;
    }

}
