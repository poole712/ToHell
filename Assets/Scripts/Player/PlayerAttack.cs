using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public float MaxChargeTime = 3f;
    public ParticleSystem slamParticle;
    public LayerMask HitLayerMask;
    public LayerMask AttackLayerMask;

    public GameObject segmentManager;
    public float MaxDamage = 15f;
    public Image PowerBar;

    private Animator _animator;
    private bool _isCharging;
    private float _power;


    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the GameObject
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player clicks right 
        if (Input.GetMouseButton(0))
        {

            // Start charging if not already charging
            if (!_isCharging)
            {
                _isCharging = true;
                // Trigger the "Hold" animation
                _animator.SetBool("Holding", true);
            }
            if (_power < MaxDamage && _isCharging)
            {
                PowerBar.fillAmount = _power / MaxDamage;
                _power += 0.005f;
                //Debug.Log(_power);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // If the player was charging, trigger the "Release" animation
            if (_isCharging)
            {
                _isCharging = false;


                _animator.SetBool("Holding", false);

            }
        }
    }

    //Can be useful for seeing where the attack sphere is located
    // void OnDrawGizmos()
    // {  
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), 3f);

    // }

    //Called in the Attack animation event!
    public void Attack()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z),
        new Vector2(Vector2.down.x + 1, Vector2.down.y), 0.1f, HitLayerMask);

        RaycastHit2D sphereHit = Physics2D.CircleCast(new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z),
        1f, new Vector2(Vector2.down.x + 5, Vector2.down.y), 0, AttackLayerMask);

        Debug.DrawRay(new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z),
        new Vector2(Vector2.down.x + 1, Vector2.down.y * 0.1f), Color.green, 3f);

        if (sphereHit.collider != null && sphereHit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            sphereHit.collider.gameObject.GetComponent<Enemy>().Speed = 0;
            Destroy(sphereHit.collider.gameObject);
        }
        else if(rayHit.collider != null && !rayHit.collider.CompareTag("Layer 5 (Bottom)"))
        {
            Instantiate(slamParticle, rayHit.point, Quaternion.identity);
            segmentManager.GetComponent<SegmentManager>().DamageLayer(_power);
            Camera.main.GetComponent<S_SimpleCamera>().Shake();
        }

        _power = 0;
        PowerBar.fillAmount = _power / MaxDamage;
    }
}
