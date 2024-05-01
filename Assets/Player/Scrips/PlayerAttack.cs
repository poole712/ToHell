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
        // if the player presses the "Jump" button (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Trigger the "Jump" animation
            _animator.SetTrigger("Jump");
        }
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
                _power += 0.01f;
                Debug.Log(_power);
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

    public void Attack()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, new Vector2(Vector2.down.x + 5, Vector2.down.y), 5, HitLayerMask);
        Debug.DrawRay(transform.position, new Vector2(Vector2.down.x + 5, Vector2.down.y * 5), Color.green, 3f);

        if (rayHit.collider != null && !rayHit.collider.CompareTag("Layer 5 (Bottom)"))
        {
            Debug.Log("Hit ground");
            Instantiate(slamParticle, rayHit.point, Quaternion.identity);
            segmentManager.GetComponent<S_SegmentManager>().DamageLayer(_power);
            Camera.main.GetComponent<S_SimpleCamera>().Shake();
            _power = 0;
            PowerBar.fillAmount = _power / MaxDamage;
        }
    }
}
