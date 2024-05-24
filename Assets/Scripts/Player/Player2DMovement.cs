using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [HideInInspector] public bool InAir;
    private Animator _animator;

    public Vector2 JumpHeight = new Vector2(1, 5f);
    public Vector2 Speed = new Vector2(5, 0);
    public float MaxSpeed = 2.5f;

    public S_SimpleCamera Camera;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Speed;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !InAir)
        {
            _animator.SetBool("Jump", true);
            InAir = true;
            rb2d.AddForce(JumpHeight, ForceMode2D.Impulse);
            Camera.Jump();
            StartCoroutine(SetInAir(true));
        }
    }

    IEnumerator SetInAir(bool toggle)
    {
        yield return new WaitForSeconds(0.25f);
        _animator.SetBool("InAir", toggle);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Debug.Log(rb2d.velocity.x);
        if (rb2d.velocity.x < MaxSpeed)
        {
            rb2d.AddForce(Speed, ForceMode2D.Force);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Landed();
        }
    }

    public void Landed()
    {
        InAir = false;
        _animator.SetBool("Jump", false);
        StartCoroutine(SetInAir(false));

    }

}
