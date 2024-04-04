using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Simple2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private Vector2 speed;
    [SerializeField] private S_SegmentManager segmentManager;

    public LayerMask HitLayerMask; 
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 50, HitLayerMask);
            Debug.DrawRay(transform.position, Vector2.down * 50, Color.green, 3f);

            if (rayHit.collider != null)
            {
                Debug.Log("Hit ground");

                segmentManager.DamageLayer(10);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(rb2d.velocity.x < 3)
        {
            rb2d.AddForce(speed);
        }
    }

}
