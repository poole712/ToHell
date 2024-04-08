using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Simple2DMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private Vector2 speed;
    public GameObject segmentManager;
    public ParticleSystem slamParticle;

    public LayerMask HitLayerMask; 
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, new Vector2(Vector2.down.x + 5, Vector2.down.y), 5, HitLayerMask);
            Debug.DrawRay(transform.position, new Vector2(Vector2.down.x + 5, Vector2.down.y * 5), Color.green, 3f);

            if (rayHit.collider != null)
            {
                Debug.Log("Hit ground");
                Instantiate(slamParticle, rayHit.point, Quaternion.identity);
                segmentManager.GetComponent<S_SegmentManager>().DamageLayer(10);
                Camera.main.GetComponent<S_SimpleCamera>().Shake();
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
