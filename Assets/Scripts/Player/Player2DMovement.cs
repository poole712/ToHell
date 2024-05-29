using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [HideInInspector] public bool InAir;
    private Animator _animator;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    public Vector2 JumpHeight = new Vector2(1, 5f);
    public Vector2 Speed = new Vector2(5, 0);
    public float MaxSpeed = 2.5f;

    public S_SimpleCamera Camera;

    //
    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Speed;
        originalSpeed = Speed.x; // Initialize original speed
    }

    private void Update()
    {

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPosition = Input.GetTouch(0).position;

            if (_endTouchPosition.y > _startTouchPosition.y && !InAir)
            {
                Jump();
            }
        }
#endif
        if (Input.GetButtonDown("Jump") && !InAir)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _animator.SetBool("Jump", true);
        InAir = true;
        rb2d.AddForce(JumpHeight, ForceMode2D.Impulse);
        Camera.Jump();
        StartCoroutine(SetInAir(true));
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
